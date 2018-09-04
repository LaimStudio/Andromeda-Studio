using AndromedaStudio.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SMenuItem = System.Windows.Controls.MenuItem;

namespace AndromedaStudio.Components
{
    public class MenuItem : Component
    {
        public string Title;

        public List<MenuItem> Items = new List<MenuItem>();

        public void Init() => Bind();

        public SMenuItem Render()
        {
            return new SMenuItem
            {
                Header = Title
            };
        }

        public void Bind()
        {
            Database.MainWindow.Dispatcher.Invoke(() =>
            {
                Database.MainWindow.PackagesMenu.Items.Add(Render());
                Database.MainWindow.PackagesMenu.Visibility = Visibility.Visible;
            });
        }

        public void UnBind()
        {
            Database.MainWindow.Dispatcher.Invoke(() =>
            {
                Database.MainWindow.PackagesMenu.Items.Remove(Render());
                if (!Database.MainWindow.PackagesMenu.HasItems)
                    Database.MainWindow.PackagesMenu.Visibility = Visibility.Collapsed;
            });
        }
    }
}
