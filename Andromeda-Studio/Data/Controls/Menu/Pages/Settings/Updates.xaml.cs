using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AndromedaStudio.Data.Controls.MenuPages.Setting
{
    public partial class Updates : Page
    {
        public Updates() => InitializeComponent();

        private void Menu_Select(object sender, RoutedEventArgs e)
        {
            Data.Classes.Menu.SetPage(sender);
        }

        async private void Checkbox(object sender, RoutedEventArgs e)
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
