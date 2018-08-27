using AndromedaStudio.Classes;
using AndromedaStudio.ViewModels;
using ICSharpCode.AvalonEdit;
using System;
using System.IO;
using System.Runtime.InteropServices;
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
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

        #region GUI

        #region Window
        void Window_SourceInitialized(object sender, EventArgs e)
        {
            IntPtr mWindowHandle = (new WindowInteropHelper(this)).Handle;
            HwndSource.FromHwnd(mWindowHandle).AddHook(new HwndSourceHook(WindowProc));
        }


        private static System.IntPtr WindowProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    WmGetMinMaxInfo(hwnd, lParam);
                    break;
            }

            return IntPtr.Zero;
        }


        private static void WmGetMinMaxInfo(System.IntPtr hwnd, System.IntPtr lParam)
        {
            GetCursorPos(out POINT lMousePosition);

            IntPtr lPrimaryScreen = MonitorFromPoint(new POINT(0, 0), MonitorOptions.MONITOR_DEFAULTTOPRIMARY);
            MONITORINFO lPrimaryScreenInfo = new MONITORINFO();
            if (GetMonitorInfo(lPrimaryScreen, lPrimaryScreenInfo) == false)
            {
                return;
            }

            IntPtr lCurrentScreen = MonitorFromPoint(lMousePosition, MonitorOptions.MONITOR_DEFAULTTONEAREST);

            MINMAXINFO lMmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

            if (lPrimaryScreen.Equals(lCurrentScreen) == true)
            {
                lMmi.ptMaxPosition.X = lPrimaryScreenInfo.rcWork.Left;
                lMmi.ptMaxPosition.Y = lPrimaryScreenInfo.rcWork.Top;
                lMmi.ptMaxSize.X = lPrimaryScreenInfo.rcWork.Right - lPrimaryScreenInfo.rcWork.Left;
                lMmi.ptMaxSize.Y = lPrimaryScreenInfo.rcWork.Bottom - lPrimaryScreenInfo.rcWork.Top;
            }
            else
            {
                lMmi.ptMaxPosition.X = lPrimaryScreenInfo.rcMonitor.Left;
                lMmi.ptMaxPosition.Y = lPrimaryScreenInfo.rcMonitor.Top;
                lMmi.ptMaxSize.X = lPrimaryScreenInfo.rcMonitor.Right - lPrimaryScreenInfo.rcMonitor.Left;
                lMmi.ptMaxSize.Y = lPrimaryScreenInfo.rcMonitor.Bottom - lPrimaryScreenInfo.rcMonitor.Top;
            }

            Marshal.StructureToPtr(lMmi, lParam, true);
        }


        private void SwitchWindowState()
        {
            switch (WindowState)
            {
                case WindowState.Normal:
                    {
                        WindowState = WindowState.Maximized;
                        break;
                    }
                case WindowState.Maximized:
                    {
                        WindowState = WindowState.Normal;
                        break;
                    }
            }
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out POINT lpPoint);


        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr MonitorFromPoint(POINT pt, MonitorOptions dwFlags);

        enum MonitorOptions : uint
        {
            MONITOR_DEFAULTTONULL = 0x00000000,
            MONITOR_DEFAULTTOPRIMARY = 0x00000001,
            MONITOR_DEFAULTTONEAREST = 0x00000002
        }


        [DllImport("user32.dll")]
        static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);


        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        };


        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MONITORINFO
        {
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));
            public RECT rcMonitor = new RECT();
            public RECT rcWork = new RECT();
            public int dwFlags = 0;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left, Top, Right, Bottom;

            public RECT(int left, int top, int right, int bottom)
            {
                this.Left = left;
                this.Top = top;
                this.Right = right;
                this.Bottom = bottom;
            }
        }
        #endregion

        private void WindowButtonsHandler(object sender, RoutedEventArgs e)
        {
            var obj = (FrameworkElement)sender;
            switch(obj.Tag)
            {
                case "Close":
                    Application.Current.Shutdown();
                    break;

                case "Maximize":
                    if (WindowState == WindowState.Normal)
                        WindowState = WindowState.Maximized;
                    else
                        WindowState = WindowState.Normal;
                    break;

                case "Minimize":
                    WindowState = WindowState.Minimized;
                    break;
            }
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
            if(Tools.IsOpened && parent.Name != "ToolsList" && (string)obj.Tag != "Project")
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
            if(obj.Items.Count == 1)
            {
                obj.InterTabController = new Dragablz.InterTabController();
                obj.Tag = 1;
            }
            else
            {
                if((int)obj.Tag == 1)
                {
                    obj.Tag = 0;
                    obj.InterTabController = new Dragablz.InterTabController();
                    Binding bind = new Binding();
                    bind.Source = DataContext;
                    bind.Path = new PropertyPath("InterTabClient");
                    bind.Mode = BindingMode.TwoWay;
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
                    MessageBox.Show("����, ���� �����!");
                    return;
                }

                var editor = new TextEditor();
                editor.Text = file;

                TabControl.SelectedIndex = TabControl.Items.Add(new Controls.TabItem
                {
                    Title = Path.GetFileName(path),
                    Content = editor,
                    Icon = icon,
                    Path = path,
                    Hash = file.GetHashCode()
                });
            }catch(Exception e)
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
