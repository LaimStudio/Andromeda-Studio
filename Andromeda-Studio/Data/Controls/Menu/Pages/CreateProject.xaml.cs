using System.Windows;
using System.Windows.Controls;

namespace AndromedaStudio.Controls.MenuPages
{
    public partial class CreateProject : Page
    {
        public CreateProject() => InitializeComponent();

        private void Menu_Select(object sender, RoutedEventArgs e)
        {
            Classes.Menu.SetPage(sender);
        }
    }
}
