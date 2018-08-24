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

            await Task.Delay(1000);

            Database.MainWindow.Show();
            Tools.Visible = false;

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
    }
}