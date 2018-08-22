using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AndromedaStudio.Classes
{
    class HeadTools
    {
        private static RadioButton _toolChecked;

        public static bool IsOpened
        {
            get => (_toolChecked is null) ? false : true;
        }

        public static async void SetPage(RadioButton sender)
        {
            var tools = Database.HeadTools;
            var toolslist = (Panel)sender.Parent;
            var window = Database.MainWindow;
            int count = 0;
            int content = 10;

            window.IsHitTestVisible = false;

            tools.SetPage((string)sender.Tag, Convert.ToSByte(count));
            await Task.Delay(1);

            count = toolslist.Children.IndexOf(sender);
            count = toolslist.Children.Count - count;

            tools.Visibility = Visibility.Visible;

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
            if (_lockHide || !IsOpened)
                return;

            _lockHide = true;

            var tools = Database.HeadTools;

            var panel = (FrameworkElement)_toolChecked.Parent;
            panel.IsHitTestVisible = false;

            _toolChecked.IsChecked = false;
            _toolChecked = null;

            await Animate.Opacity(tools, 0);
            tools.SetPage(null, 0);
            tools.Visibility = Visibility.Collapsed;

            panel.IsHitTestVisible = true;
            _lockHide = false;
        }
    }
}
