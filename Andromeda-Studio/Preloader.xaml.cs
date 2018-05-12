using AndromedaStudio.Data.Classes;
using System.Windows;

namespace AndromedaStudio
{
    public partial class Preloader : Window
    {
        public Preloader()
        {
            InitializeComponent();

            Database.MainWindow.Show();
            Hide();
        }
    }
}
