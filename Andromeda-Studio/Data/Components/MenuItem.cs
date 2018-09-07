using AndromedaStudio.Classes;
using IronPython.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using SMenuItem = System.Windows.Controls.MenuItem;

namespace AndromedaStudio.Components
{
    public class MenuItem : Component, IDisposable
    {
        public string Title;

        public IList<object> Items;

        public SMenuItem Render()
        {
            var result = new SMenuItem { Header = Title };
            if (Items != null)
                foreach (var child in Items.Cast<PythonDictionary>())
                    result.Items.Add(Parse(child.ToList()).Cast<MenuItem>().Render());
            return result;
        }

        public void Bind()
        {
            Database.MainWindow.Dispatcher.Invoke(() =>
            {
                Database.MainWindow.PackagesMenu.Items.Add(Render());
                Database.MainWindow.PackagesMenu.Visibility = Visibility.Visible;
            });
        }

        public void Dispose() => Bind();
    }
}
