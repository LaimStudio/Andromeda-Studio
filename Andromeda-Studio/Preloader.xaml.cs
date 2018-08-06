using AndromedaStudio.Classes;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace AndromedaStudio
{
    public partial class Preloader : Window
    {
        public Preloader()
        {
            LoadInterface();
            LoadPackages();
            Hide();
        }

        private void LoadInterface()
        {
            Database.MainWindow.Body.Children.Add(Database.HeadTools);
            Database.MainWindow.Body.Children.Add(Database.Tools);       //🤔 бля хуйня смайлик
            Database.MainWindow.Body.Children.Add(Database.Menu);        //сейчас бы комментировать код смайликами
            Database.MainWindow.Show();                                  //сейчас бы комментировать код. давай так и оставим, все равно никто не смотрит

            Tools.Visible = false;
        }

        private async void LoadPackages()
        {
            var loader = new PackageLoader();
            var path = Path.Combine(System.Environment.CurrentDirectory, "Packages");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            await loader.LoadFromDirectory(path);
            loader.Init();
        }
    }
}