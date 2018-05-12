using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Threading.Tasks;

namespace AndromedaStudio.Data.Classes
{
    class Tools
    {
        private static bool _lock = false;
        private static bool _autoHidden = true;
        private static bool _visible = false;

        public static bool AutoHidden
        {
            get => _autoHidden;
            set
            {
                _autoHidden = value;
                if(value == false && _visible == false)
                {
                    VisibleAnimation(true);
                }
                if (value == true && _visible == false)
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
                if(AutoHidden == true)
                {
                    VisibleAnimation(value);
                }
            }
        }

        private async static void VisibleAnimation(bool type)
        {
            if (_lock)
                return;
            _lock = true;
            var window = Database.MainWindow;
            if(type)
            {
                Animate.Opacity(window.ToolsContent, 1);
                Animate.Opacity(window.ToolsCircles, 0);
                await Task.Delay(300);
                window.ToolsCircles.Children.Clear();
                await Task.Delay(100);

                _lock = false;
                if(_visible == false)
                {
                    VisibleAnimation(false);
                }
            }
            else
            {
                Animate.Opacity(window.ToolsContent, 0);
                Animate.Opacity(window.ToolsCircles, 1);

                int count = Database.MainWindow.ToolsContent.Children.Count;
                while (count != 0)
                {
                    count--;
                    Database.MainWindow.ToolsCircles.Children.Add(new Controls.RadioButton());
                }

                await Task.Delay(500);
                _lock = false;

                if (_visible == true)
                {
                    VisibleAnimation(true);
                }
            }
        }

    }

    class Animate
    {
        public static void Opacity(FrameworkElement sender, double value)
        {
            sender.BeginAnimation(Control.OpacityProperty, new DoubleAnimation
            {
                To = value,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.7,
                Duration = TimeSpan.FromMilliseconds(300)
            });
        }
    }
}
