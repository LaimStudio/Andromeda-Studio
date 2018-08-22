using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AndromedaStudio.Controls.MenuPages.Setting
{
    public partial class Account : Page
    {
        public Account() => InitializeComponent();

        private void Menu_Select(object sender, RoutedEventArgs e)
        {
            Classes.Menu.SetPage(sender);
        }
    }
}
