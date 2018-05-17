using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Threading.Tasks;

namespace AndromedaStudio.Data.Classes
{
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
                _autoHidden = value;
                if(!value && !Visible)
                {
                    VisibleAnimation(true);
                }
                if (value && !Visible)
                {
                    VisibleAnimation(false);
                }
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
            if(type)
            {
                Animate.Opacity(window.ToolsList, 1);
                Animate.Opacity(window.ToolsCircles, 0);
                await Task.Delay(300);
                window.ToolsCircles.Children.Clear();

                await Task.Delay(100);
                _lock = false;

                if(!Visible)
                {
                    VisibleAnimation(false);
                }
            }
            else
            {
                if (!AutoHidden || IsOpened)
                {
                    AutoHidden = _lock = false;
                    Visible = true;
                    return;
                }

                _lock = true;
                await Task.Delay(3000);

                if (Visible)
                {
                    _lock = false;
                    return;
                }

                Animate.Opacity(window.ToolsList, 0);
                Animate.Opacity(window.ToolsCircles, 1);

                byte count = (byte)Database.MainWindow.ToolsList.Children.Count;
                while (count != 1)
                {
                    count--;
                    Database.MainWindow.ToolsCircles.Children.Add(new Controls.RadioButton());
                }

                Database.MainWindow.ToolsCircles.Children.Add(new Separator { Margin = new Thickness(14,3,14,3), Opacity = 0.5 });

                await Task.Delay(500);
                _lock = false;

                if (Visible)
                {
                    VisibleAnimation(true);
                }
            }
        }

        public static void SetPage(RadioButton sender)
        {
            var tools = Database.Tools;
            var toolslist = (StackPanel)sender.Parent;
            int bottomArrow = 0;
            int bottomContent = 10;

            if (toolslist.Name != "ToolsList")
            {
                bottomArrow = 10;
            }
            else
            {
                var count = toolslist.Children.IndexOf(sender);
                count = toolslist.Children.Count - count - 1;
                bottomArrow = 17 + 40 * count;
                if(bottomArrow > 150)
                {
                    bottomContent += bottomArrow - 150;
                    bottomArrow = 150;
                }
            }

            tools.Page = (string)sender.Tag;
            tools.Margin = new Thickness(0, 0, 55, bottomContent);
            tools.Arrow.Margin = new Thickness(0, 0, 0, bottomArrow+5);
            tools.Visibility = Visibility.Visible;
            _toolChecked = sender;
        }

        public static void HideContent()
        {
            var tools = Database.Tools;

            tools.Page = null;
            tools.Visibility = Visibility.Collapsed;
            _toolChecked.IsChecked = false;
            _toolChecked = null;
            AutoHidden = _autoHiddenSetting;
        }

    }

    class Animate
    {
        public static void Opacity(FrameworkElement sender, double value)
        {
            sender.BeginAnimation(UIElement.OpacityProperty, new DoubleAnimation
            {
                To = value,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.7,
                Duration = TimeSpan.FromMilliseconds(300)
            });
        }
    }
}
