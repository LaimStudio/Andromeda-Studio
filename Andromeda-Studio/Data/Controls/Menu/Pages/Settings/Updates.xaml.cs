using System.Windows;
using System.Windows.Controls;

namespace AndromedaStudio.Controls.MenuPages.Setting
{
    public partial class Updates : Page
    {
        public Updates() => InitializeComponent();

        private void Menu_Select(object sender, RoutedEventArgs e)
        {
            Classes.Menu.SetPage(sender);
        }

        private void Checkbox(object sender, RoutedEventArgs e)
        {
            var obj = (CheckBox)sender;
            var par = (string)obj.Tag;

            if (obj.IsChecked == true)
            {
            }
            
            if (obj.IsChecked == false)
            {
            }
        }
    }
}
