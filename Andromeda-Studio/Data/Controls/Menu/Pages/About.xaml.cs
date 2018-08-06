using System.Windows.Controls;
using System.Diagnostics;
using System.Windows;

namespace AndromedaStudio.Controls.MenuPages
{
    public partial class About : Page
    {
        public About() => InitializeComponent();

        private void Open_Url(object sender, RoutedEventArgs e)
        {
            var obj = (Button)sender;
            Process.Start((string)obj.Tag);
        }

        private void Menu_Select(object sender, RoutedEventArgs e)
        {
            Classes.Menu.SetPage(sender);
        }
    }
}
