using AndromedaStudio.Classes;
using ICSharpCode.AvalonEdit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndromedaStudio.Classes.PackageApi
{
    public class Editor
    {
        public void OpenFile(string path)
        {
            Database.MainWindow.Dispatcher.Invoke(() =>
            {
                Database.MainWindow.OpenFile(@path);
            });
        }

        public void Insert(int position, string text)
        {
            Database.MainWindow.Dispatcher.Invoke(() =>
            {
                var editor = (TextEditor)Database.MainWindow.TabControl.SelectedContent;
                editor.Text.Insert(position, text);
            });
        }

        public void SaveFile(string path)
        {
            Database.MainWindow.Dispatcher.Invoke(() =>
            {
                var editor = (TextEditor)Database.MainWindow.TabControl.SelectedContent;
                File.WriteAllText(@path, editor.Text);
            });
        }
    }
}
