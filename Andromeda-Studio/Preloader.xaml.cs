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
            Hide();
            LoadInterface();
            await LoadPackages();

            Database.MainWindow.Show();
            Tools.Visible = false;
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