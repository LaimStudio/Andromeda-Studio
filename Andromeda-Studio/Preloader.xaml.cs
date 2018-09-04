using AndromedaStudio.Classes;
using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace AndromedaStudio
{
    public partial class Preloader : Window
    {
        public Preloader() => Init();

        [DllImport("user32.dll")]
        #pragma warning disable IDE1006
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, IntPtr dwExtraInfo);
        #pragma warning restore IDE1006

        public async void Init()
        {
            var Settings = Database.Settings;
            
            App.Theme = Settings.Theme;
            App.AltColor = Settings.AltColor;

            Settings.Theme = App.Theme;
            Settings.AltColor = App.AltColor;

            Width = Settings.Window.Width;
            Height = Settings.Window.Height;

            if(Settings.Language == null)
                Settings.Language = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

            try
            {
                App.Language = new CultureInfo(Settings.Language);
            }
            catch(CultureNotFoundException)
            {
                Settings.Language = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
                App.Language = new CultureInfo(Settings.Language);
            }
            Settings.Language = App.Language.ToString();

            Animation();
            await LoadInterface();
            await LoadPackages();
            await Finish();

            await Animate.Opacity(this, 0, 400);

            Close();
        }

        private Task LoadInterface()
        {
            var mainWindow = Database.MainWindow;
            mainWindow.Body.Children.Add(Database.HeadTools);
            mainWindow.Body.Children.Add(Database.Tools);
            mainWindow.Body.Children.Add(Database.Menu);

            mainWindow.Opacity = 0;
            mainWindow.Show();
            return Task.CompletedTask;
        }

        private async Task LoadPackages()
        {
            var path = Path.Combine(Environment.CurrentDirectory, "Packages");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            await Database.PackageLoader.LoadFromDirectory(path);
        }

        private async Task Finish()
        {
            var Settings = Database.Settings;
            var mainWindow = Database.MainWindow;

            mainWindow.WindowState = WindowState;

            IsHitTestVisible = false;
            await Task.Delay(100);
            mouse_event(0x0004, 0, 0, 0, IntPtr.Zero);
            await Task.Delay(100);

            if (!Database.Settings.Window.IsMaximized)
            {
                mainWindow.Width = Width;
                mainWindow.Height = Height;
                mainWindow.Top = Top;
                mainWindow.Left = Left;
            }

            Focus();
            mainWindow.Opacity = 1;
            Tag = "Shadow";

            Tools.Visible = false;
        }

        private async void Animation()
        {
            await Task.Delay(100);
            while (true)
            {
                await Animate.Opacity(WindowContent, 0.6, 550);
                await Animate.Opacity(WindowContent, 1, 550);
            }
        }

        #region Window
        void Window_SourceInitialized(object sender, EventArgs e)
        {
            MaximizedToWSNone.Set(this);
            if (Database.Settings.Window.IsMaximized)
                WindowState = WindowState.Maximized;
        }
        #endregion
    }
}