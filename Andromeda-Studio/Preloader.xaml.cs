using AndromedaStudio.Classes;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;

namespace AndromedaStudio
{
    public partial class Preloader : Window
    {
        public Preloader() => Init();

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, IntPtr dwExtraInfo);

        public async void Init()
        {
            var Settings = Database.Settings;

            Top = Settings.Window.Top;
            Left = Settings.Window.Left;
            Width = Settings.Window.Width;
            Height = Settings.Window.Height;

            if (Settings.Window.IsMaximized)
                WindowState = WindowState.Maximized;

            Animation();
            LoadInterface();
            await LoadPackages();

            await Task.Delay(3000);

            var mainWindow = Database.MainWindow;
            mainWindow.Width = Width;
            mainWindow.Height = Height;
            mainWindow.Top = Top;
            mainWindow.Left = Left;
            mainWindow.WindowState = WindowState;

            IsHitTestVisible = false;
            mouse_event(0x0004, 0, 0, 0, IntPtr.Zero);
            Focus();
            mainWindow.Opacity = 1;
            Tag = "Shadow";

            Tools.Visible = false;
            await Animate.Opacity(this, 0, 400);
            Close();
        }

        private void LoadInterface()
        {
            var mainWindow = Database.MainWindow;
            mainWindow.Body.Children.Add(Database.HeadTools);
            mainWindow.Body.Children.Add(Database.Tools);
            mainWindow.Body.Children.Add(Database.Menu);

            mainWindow.Opacity = 0;
            mainWindow.Show();
        }

        private async Task LoadPackages()
        {
            var path = Path.Combine(System.Environment.CurrentDirectory, "Packages");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            await Database.PackageLoader.LoadFromDirectory(path);
        }

        private async void Animation()
        {
            await Task.Delay(100);
            while (true)
            {
                await Animate.Opacity(Content, 0.6, 550);
                await Animate.Opacity(Content, 1, 550);
            }
        }
    }
}