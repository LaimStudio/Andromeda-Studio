using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AndromedaStudio.Classes
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
                    Database.MainWindow.ToolsCircles.Children.Add(new Controls.Icon());
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
                bottomArrow = 17 + (40 * count);
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
            if (_lockHide || !IsOpened)
                return;

            _lockHide = true;

            var tools = Database.Tools;

            var panel = (FrameworkElement)_toolChecked.Parent;
            panel.IsHitTestVisible = false;

            _toolChecked.IsChecked = false;
            _toolChecked = null;
            AutoHidden = _autoHiddenSetting;

            await Animate.Opacity(tools, 0);
            tools.SetPage(null, 0);
            tools.Visibility = Visibility.Collapsed;

            panel.IsHitTestVisible = true;
            _lockHide = false;
        }
    }
}
