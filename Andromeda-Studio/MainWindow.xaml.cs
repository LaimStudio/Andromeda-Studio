using AndromedaStudio.Classes;
using AndromedaStudio.ViewModels;
using ICSharpCode.AvalonEdit;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;

namespace AndromedaStudio
{
    public partial class MainWindow : Window
    {
#pragma warning disable CS4014

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            RunPeriodicSave();
        }

        #region GUI

        #region Window
        void Window_SourceInitialized(object sender, EventArgs e)
        {
            MaximizedToWSNone.Set(this);
            if (Database.Settings.Window.IsMaximized)
                WindowState = WindowState.Maximized;
        }
        #endregion

        private void WindowButtonsHandler(object sender, RoutedEventArgs e)
        {
            Classes.WindowButtonsHandler.Set(this, (FrameworkElement)sender);
        }

        #endregion

        #region Tools

        private void Tools_MouseEnter(object sender, MouseEventArgs e)
        {
            Tools.Visible = true;
        }

        private void Tools_MouseLeave(object sender, MouseEventArgs e)
        {
            Tools.Visible = false;
        }

        private void Tools_Selected(object sender, RoutedEventArgs e)
        {
            var obj = (RadioButton)e.Source;
            if (obj.IsChecked == true)
                Tools.SetPage(obj);
            else
                Tools.HideContent();
        }

        private void ContentFocus(object sender, MouseButtonEventArgs e)
        {
            var obj = (FrameworkElement)e.Source;
            var parent = (FrameworkElement)obj.Parent;
            if (Tools.IsOpened && parent.Name != "ToolsList" && (string)obj.Tag != "Project")
            {
                Tools.HideContent();
                Tools.Visible = false;
            }

            if (HeadTools.IsOpened && parent.Name != "HeadMenuPanel")
            {
                HeadTools.HideContent();
            }
        }

        #endregion

        #region Save

        async Task RunPeriodicSave()
        {
            while (true)
            {
                await Task.Delay(10000);
                Save();
            }
        }

        private void Save()
        {
            var Settings = Database.Settings;
            var mainWindow = Database.MainWindow;

            Settings.Window.Width = mainWindow.Width;
            Settings.Window.Height = mainWindow.Height;
            if (mainWindow.WindowState == WindowState.Maximized)
                Settings.Window.IsMaximized = true;
            else
                Settings.Window.IsMaximized = false;

            Database.Settings.Save();
        }

        #endregion

        private void Menu_Select(object sender, RoutedEventArgs e)
        {
            Classes.Menu.SetPage(sender);
        }

        private void TestNotice(object sender, RoutedEventArgs e)
        {
            var notice = new Notifications.Notification
            {
                Icon = (Geometry)TryFindResource("ComponentIcon"),
                Content = "Test caption",
                Description = "Test description"
            };
            Database.NotificationsManager.Add(notice);
        }

        private void ExecutePython(object sender, RoutedEventArgs e)
        {
            //Database.MainWindow.
        }

        private void TabControlSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var obj = (Dragablz.TabablzControl)sender;
            if (obj.Items.Count == 1)
            {
                obj.InterTabController = new Dragablz.InterTabController();
                obj.Tag = 1;
            }
            else
            {
                if ((int)obj.Tag == 1)
                {
                    obj.Tag = 0;
                    obj.InterTabController = new Dragablz.InterTabController();
                    Binding bind = new Binding
                    {
                        Source = DataContext,
                        Path = new PropertyPath("InterTabClient"),
                        Mode = BindingMode.TwoWay
                    };
                    obj.InterTabController.SetBinding(Dragablz.InterTabController.InterTabClientProperty, bind);
                }
            }
        }

        public void RemoveTestPackage(object sender, RoutedEventArgs e)
        {
            Database.PackageLoader.Packages.RemoveAll(x => x.Name == "TestPackage");
            RemoveTestPackageButton.IsEnabled = false;
        }

        public void OpenFile(string path)
        {
            foreach (TabItem tab in TabControl.Items)
            {
                if ((string)tab.Tag == path)
                {
                    TabControl.SelectedItem = tab;
                    return;
                }
            }

            try
            {
                var icon = (Geometry)TryFindResource("FileIcon");
                var file = File.ReadAllText(path);
                var ext = Path.GetExtension(path).Remove(0, 1);

                if (ext == "putin")
                {
                    MessageBox.Show("боже, царя храни!");
                    return;
                }

                var editor = new TextEditor
                {
                    Text = file
                };

                TabControl.SelectedIndex = TabControl.Items.Add(new Controls.TabItem
                {
                    Title = Path.GetFileName(path),
                    Content = editor,
                    Icon = icon,
                    Path = path,
                    Hash = file.GetHashCode()
                });
            }
            catch (Exception e)
            {
                var notice = new Notifications.Notification
                {
                    Icon = (Geometry)TryFindResource("AlertCircleIcon"),
                    Content = "Exception",
                    Description = e.Message
                };
                Database.NotificationsManager.Add(notice);
            }
        }
    }
}
