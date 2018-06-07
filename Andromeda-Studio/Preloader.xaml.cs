using AndromedaStudio.Data.Classes;
using System.Windows;

namespace AndromedaStudio
{
    public partial class Preloader : Window
    {
        public Preloader()
        {
            Database.MainWindow.Body.Children.Add(Database.Tools);
            Database.MainWindow.Body.Children.Add(Database.Menu);
            Database.MainWindow.Show();

            Tools.Visible = false;
            Hide();
        }
    }
}
