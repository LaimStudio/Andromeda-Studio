using AndromedaStudio.Classes.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AndromedaStudio.Classes.PackageApi
{
    public class Menu
    {
        private MenuItem _root = new MenuItem { Header = "test" };

        public ItemCollection Items
        {
            get => _root.Items;
            set
            {
                Database.MainWindow.PackagesMenu.Items.Remove(_root);
                foreach (var item in value)
                    _root.Items.Add(item);
                Database.MainWindow.PackagesMenu.Items.Add(_root);
            }
        }
    }
}