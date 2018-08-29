using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Threading.Tasks;
using System.Windows.Media;
using AndromedaStudio.Classes;

namespace AndromedaStudio.Controls.MenuPages
{
    public partial class Open : Page
    {
        #pragma warning disable CS4014

        List<TreeViewItem> opened = new List<TreeViewItem>();

        public Open()
        {
            InitializeComponent();
            loadDisks();
        }

        async Task loadDisks()
        {
            var disks = new List<TreeViewItem>();
            Files.Items.Add(new TreeViewItem() { Header = (string)TryFindResource("@Loading") + "..", Opacity = 0.7 });
            await Task.Delay(1);

            foreach (string drive in Environment.GetLogicalDrives())
            {
                TreeViewItem item = new TreeViewItem() { Header = drive, Tag = drive };
                item.Items.Add(new TreeViewItem() { Header = (string)TryFindResource("@Loading") + "..", Opacity = 0.7 });
                disks.Add(item);
            }

            Files.Items.Clear();
            foreach (TreeViewItem item in disks)
                Files.Items.Add(item);
        }

        private void Files_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        }

        async Task FilesGet(TreeViewItem obj)
        {
            var path = (string)obj.Tag;
            var files = new List<TreeViewItem>();
            var dirinfo = new DirectoryInfo(path);
            opened.Add(obj);
            obj.IsExpanded = true;
            obj.Focus();

            await Task.Delay(1);

            try
            {
                foreach (DirectoryInfo dir in dirinfo.GetDirectories())
                {
                    TreeViewItem tree = new TreeViewItem() { Header = dir.Name, Tag = path + dir.Name + "\\" };
                    tree.Items.Add(new TreeViewItem() { Header = (string)TryFindResource("@Loading") + "..", Opacity = 0.7 });
                    if (dir.Attributes.HasFlag(FileAttributes.Hidden))
                            tree.Opacity = 0.5;
                        files.Add(tree);
                }

                foreach (FileInfo file in dirinfo.GetFiles())
                {
                    TreeViewItem tree = new TreeViewItem() { Header = file.Name, Tag = path + file.Name, DataContext = (Geometry)TryFindResource("FileIcon")};
                    if (file.Attributes.HasFlag(FileAttributes.Hidden))
                        tree.Opacity = 0.5;
                    files.Add(tree);
                }

                obj.Items.Clear();
                foreach (TreeViewItem item in files)
                    obj.Items.Add(item);
            }
            catch (Exception)
            {
                obj.Items.Clear();
                return;
            }
        }

        void FilesClear(TreeViewItem obj)
        {
            opened.Remove(obj);
            obj.Items.Clear();
            obj.Items.Add(new TreeViewItem() { Header = (string)TryFindResource("@Loading") + "..", Opacity = 0.7 });
        }

        async void PathFind(string path)
        {
            if (Directory.Exists(path))
            {
                string[] folders = path.Split(new char[] { '\\', '/' });

                opened.Clear();
                Files.Items.Clear();

                foreach (string folder in folders)
                {
                    if (Files.Items.Count == 0)
                    {
                        await loadDisks();
                        foreach (TreeViewItem item in Files.Items)
                        {
                            if (item.Header.ToString().ToLower() == folder.ToLower() + "\\")
                                await FilesGet(item);
                        }
                    }
                    else
                    {
                        foreach (TreeViewItem item in opened[opened.Count - 1].Items)
                        {
                            if (item.Header.ToString().ToLower() == folder.ToLower())
                                await FilesGet(item);
                        }
                    }
                }
            }
            else if (File.Exists(path))
            {
                OpenFile(path);
            }
            else Path.Error = true;
        }

        private void Files_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem obj = e.Source as TreeViewItem;
            FilesGet(obj);
        }

        private void Files_Collapsed(object sender, RoutedEventArgs e)
        {
            TreeViewItem obj = e.Source as TreeViewItem;
            FilesClear(obj);
        }

        private void Files_Selected(object sender, RoutedEventArgs e)
        {
            TreeViewItem obj = e.Source as TreeViewItem;
            Path.Text = (string)obj.Tag;
        }

        private void Path_KeyUp(object sender, KeyEventArgs e)
        {
            var obj = (TextBox)sender;
            obj.Error = false;
            if (e.Key == Key.Enter)
            {
                PathFind(obj.Text);
            }
        }

        private void Shortcut(object sender, RoutedEventArgs e)
        {
            var obj = e.Source as FrameworkElement;
            switch(obj.Tag)
            {
                case ("Documents"):
                    PathFind(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                    break;
                case ("User"):
                    PathFind(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile));
                    break;
                case ("Desktop"):
                    PathFind(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
                    break;
            }
        }

        private void OpenPath(object sender, RoutedEventArgs e)
        {
            var path = Path.Text;

            if (Directory.Exists(path))
            {
                //хы
            }
            else if (File.Exists(path))
            {
                OpenFile(path);
            }
        }

        void OpenFile(string path)
        {
            Classes.Menu.SetPage(null);
            Database.MainWindow.OpenFile(path);
        }
    }
}
