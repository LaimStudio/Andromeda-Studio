using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows.Data;
using System.Collections.Generic;
using AndromedaStudio.Data.Classes;
using System.Windows.Media;

namespace AndromedaStudio.Data.Classes
{

#pragma warning disable CS4014

    class Tools
    {
        private static bool _lock;
        private static bool _visible;
        private static RadioButton _toolChecked;
        private static bool _autoHidden = true;
        private static bool _autoHiddenSetting = _autoHidden;

        public static bool IsOpened
        {
            get => (_toolChecked is null) ? false : true;
        }

        public static bool AutoHidden
        {
            get => _autoHidden;
            set
            {
                if (!value)
                    Visible = !value;
                _autoHidden = value;
            }
        }

        public static bool Visible
        {
            get => _visible;
            set
            {
                _visible = value;
                if (AutoHidden)
                {
                    VisibleAnimation(value);
                }
            }
        }

        private static async void VisibleAnimation(bool type)
        {
            if (_lock)
                return;

            _lock = true;
            var window = Database.MainWindow;
            if (type)
            {
                Animate.Opacity(window.ToolsList, 1, 300);
                await Animate.Opacity(window.ToolsCircles, 0);
                window.ToolsCircles.Children.Clear();
                _lock = false;

                if (!Visible)
                {
                    VisibleAnimation(false);
                }
            }
            else
            {
                if (!AutoHidden)
                {
                    _lock = false;
                    return;
                }

                _lock = true;
                await Task.Delay(3000);

                if (Visible || IsOpened)
                {
                    _lock = false;
                    return;
                }

                byte count = (byte)Database.MainWindow.ToolsList.Children.Count;
                while (count != 1)
                {
                    count--;
                    Database.MainWindow.ToolsCircles.Children.Add(new Controls.RadioButton());
                }

                Database.MainWindow.ToolsCircles.Children.Add(new Separator { Margin = new Thickness(14, 3, 14, 3), Opacity = 0.5 });

                Animate.Opacity(window.ToolsList, 0, 300);
                await Animate.Opacity(window.ToolsCircles, 1, 300);
                _lock = false;

                if (Visible)
                {
                    VisibleAnimation(true);
                }
            }
        }

        public static async void SetPage(RadioButton sender)
        {
            var tools = Database.Tools;
            var window = Database.MainWindow;
            var toolslist = (Panel)sender.Parent;
            int bottomArrow = 0;
            int count = 0;
            int bottomContent = 10;
            window.IsHitTestVisible = false;
            if (toolslist.Name != "ToolsList")
            {
                bottomArrow = 10;
            }
            else
            {
                count = toolslist.Children.IndexOf(sender);
                count = toolslist.Children.Count - count - 1;
                bottomArrow = 17 + 40 * count;
                if (bottomArrow > 140)
                {
                    bottomContent += bottomArrow - 140;
                    bottomArrow = 140;
                }
            }

            tools.SetPage((string)sender.Tag, Convert.ToSByte(count));

            if (!IsOpened)
            {
                _toolChecked = sender;

                tools.Margin = new Thickness(0, 0, 55, bottomContent);
                tools.Arrow.Margin = new Thickness(0, 0, -2, bottomArrow + 5);
                tools.Visibility = Visibility.Visible;

                tools.Opacity = 0;
                await Animate.Opacity(tools, 1);
            }
            else
            {
                _toolChecked = sender;

                Animate.Margin(tools, new Thickness(0, 0, 55, bottomContent));
                await Animate.Margin(tools.Arrow, new Thickness(0, 0, -2, bottomArrow + 5));
            }
            window.IsHitTestVisible = true;
        }

        private static bool _lockHide = false;
        public static async void HideContent()
        {
            if (_lockHide)
                return;

            var window = Database.MainWindow;
            window.IsHitTestVisible = false;

            _lockHide = true;

            var tools = Database.Tools;

            _toolChecked.IsChecked = false;
            _toolChecked = null;
            AutoHidden = _autoHiddenSetting;

            await Animate.Opacity(tools, 0);
            tools.SetPage(null, 0);
            tools.Visibility = Visibility.Collapsed;

            window.IsHitTestVisible = true;
            _lockHide = false;
        }
    }

    class HeadTools
    {
        private static RadioButton _toolChecked;

        public static bool IsOpened
        {
            get => (_toolChecked is null) ? false : true;
        }

        public static async void SetPage(RadioButton sender)
        {
            var window = Database.MainWindow;
            window.IsHitTestVisible = false;

            var tools = Database.HeadTools;
            var toolslist = (Panel)sender.Parent;
            int count = 0;
            int content = 10;

            tools.SetPage((string)sender.Tag, Convert.ToSByte(count));
            tools.Visibility = Visibility.Visible;
            await Task.Delay(1);

            count = toolslist.Children.IndexOf(sender);
            count = toolslist.Children.Count - count;

            if (!IsOpened)
            {
                _toolChecked = sender;

                tools.Margin = new Thickness(0, 31, content + 30, 0);

                tools.Opacity = 0;
                await Animate.Opacity(tools, 1);
            }
            else
            {
                _toolChecked = sender;

                await Animate.Margin(tools, new Thickness(0, 31, content + 30, 0));
            }
            window.IsHitTestVisible = true;
        }

        private static bool _lockHide = false;
        public static async void HideContent()
        {
            if (_lockHide)
                return;

            var window = Database.MainWindow;
            window.IsHitTestVisible = false;

            _lockHide = true;

            var tools = Database.HeadTools;

            _toolChecked.IsChecked = false;
            _toolChecked = null;

            await Animate.Opacity(tools, 0);
            tools.SetPage(null, 0);
            tools.Visibility = Visibility.Collapsed;

            window.IsHitTestVisible = true;
            _lockHide = false;
        }
    }

    class Menu
    {
        public static bool IsOpened
        {
            get;
            private set;
        }

        public static void SetPage(object obj)
        {
            if (obj == null)
            {
                VisibleAnimation(false);
                IsOpened = false;
                return;
            }

            var menu = Database.Menu;
            var sender = (FrameworkElement)obj;
            var value = (string)sender.Tag;
            string arg = null;

            if (value.IndexOf(".") != -1)
            {
                string[] values = value.Split('.');
                value = values[0];
                arg = values[1];
            }

            menu.SetPage(value, arg);

            if (!IsOpened)
            {
                VisibleAnimation(true);
                IsOpened = true;
            }
        }

        private static async void VisibleAnimation(bool type)
        {
            var menu = Database.Menu;
            var mainWindow = Database.MainWindow;
            if (type)
            {
                menu.Opacity = 0;
                menu.Visibility = Visibility.Visible;
                Animate.Opacity(menu, 1);
                Animate.Opacity(mainWindow.WindowContent, 0.5);
                mainWindow.WindowContent.IsHitTestVisible = false;
            }
            else
            {
                menu.Opacity = 1;
                Animate.Opacity(mainWindow.WindowContent, 1);
                await Animate.Opacity(menu, 0);
                menu.SetPage(null, null);
                menu.Visibility = Visibility.Collapsed;
                mainWindow.WindowContent.IsHitTestVisible = true;
            }
        }
    }

    class Animate
    {
        public static async Task Opacity(FrameworkElement sender, double value, int time = 200)
        {
            sender.BeginAnimation(FrameworkElement.OpacityProperty, new DoubleAnimation
            {
                To = value,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.7,
                Duration = TimeSpan.FromMilliseconds(time)
            });
            await Task.Delay(time);
            sender.Opacity = value;
            sender.BeginAnimation(UIElement.OpacityProperty, null);
        }

        public static async Task Margin(FrameworkElement sender, Thickness value, int time = 200)
        {
            sender.BeginAnimation(FrameworkElement.MarginProperty, new ThicknessAnimation
            {
                To = value,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.7,
                Duration = TimeSpan.FromMilliseconds(time)
            });
            await Task.Delay(time);
            sender.Margin = value;
            sender.BeginAnimation(FrameworkElement.MarginProperty, null);
        }

        public static async Task Size(FrameworkElement sender, double width, double height, int time = 200)
        {
            bool a = false;
            bool b = false;

            if (width != sender.ActualWidth)
            {
                sender.Width = sender.ActualWidth;
                sender.BeginAnimation(FrameworkElement.WidthProperty, new DoubleAnimation
                {
                    To = width,
                    AccelerationRatio = 0.2,
                    DecelerationRatio = 0.7,
                    Duration = TimeSpan.FromMilliseconds(time)
                });
                a = true;
            }

            if (height != sender.ActualHeight)
            {
                sender.Height = sender.ActualHeight;
                sender.BeginAnimation(FrameworkElement.HeightProperty, new DoubleAnimation
                {
                    To = height,
                    AccelerationRatio = 0.2,
                    DecelerationRatio = 0.7,
                    Duration = TimeSpan.FromMilliseconds(time)
                });
                b = true;
            }
            if (!a && !b)
                return;

            await Task.Delay(time);
        }
    }

    public class StringToUpperCaseConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((string)value).ToUpper();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class WidthAndHeightToRectConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double width = (double)values[0];
            double height = (double)values[1];
            return new Rect(0, 0, width, height);
        }
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

namespace AndromedaStudio.Notifications
{
    public class Notification : Data.Controls.Button
    {
        #region Properties

        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        #region DependencyProperties

        public readonly static DependencyProperty DescriptionProperty =
             DependencyProperty.Register("Description", typeof(string),
             typeof(Notification));

        #endregion

        #endregion
    }

    public class Manager
    {
        public List<Notification> Notifications = new List<Notification>();

        public void Add(Notification obj)
        {
            Notifications.Insert(0, obj);
            if (!(Database.HeadTools.Page == "Notification" && HeadTools.IsOpened))
                Database.MainWindow.ProfileButton.New = true;
            Database.NotificationsPanel.Add(obj);
        }

        async public void Remove(Notification obj)
        {
            Animate.Opacity(obj, 0, 300);
            Notifications.Remove(obj);
            if (Notifications.Count == 0)
            {
                Database.MainWindow.ProfileButton.New = false;
                Database.NotificationsPanel.Cleared();
            }

            await Animate.Size(obj, 0, 0, 300);
            Database.NotificationsPanel.Notifications.Children.Remove(obj);
        }
    }
}
