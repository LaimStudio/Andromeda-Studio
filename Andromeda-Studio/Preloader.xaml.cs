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
            Animation();
            LoadInterface();
            await LoadPackages();

            var mainWindow = Database.MainWindow;
            mainWindow.Opacity = 0;
            IsHitTestVisible = false;

            await Task.Delay(50);
            mouse_event(0x0004, 0, 0, 0, IntPtr.Zero);
            await Task.Delay(50);

            mainWindow.Top = Top;
            mainWindow.Left = Left;
            mainWindow.Show();
            Focus();

            mainWindow.Opacity = 1;
            Tag = "Shadow";
            Tools.Visible = false;

            await Animate.Opacity(this, 0, 400);
            Close();
        }

        private void LoadInterface()
        {
            Database.MainWindow.Body.Children.Add(Database.HeadTools);
            Database.MainWindow.Body.Children.Add(Database.Tools);
            Database.MainWindow.Body.Children.Add(Database.Menu);
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