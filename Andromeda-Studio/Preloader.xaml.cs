using AndromedaStudio.Classes;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace AndromedaStudio
{
    public partial class Preloader : Window
    {
        public Preloader() => Init();

        public async void Init()
        {
            LoadInterface();
            await LoadPackages();
            Animation();

            await Task.Delay(2000);

            var mainWindow = Database.MainWindow;
            mainWindow.Top = Top;
            mainWindow.Left = Left;

            Tag = "Shadow";
            mainWindow.Show();
            Focus();

            mainWindow.Opacity = 1;
            Tools.Visible = false;

            await Animate.Opacity(this, 0, 400);
            Hide();
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
            while (true)
            {
                await Animate.Opacity(Content, 0.6, 550);
                await Animate.Opacity(Content, 1, 550);
            }
        }
    }
}