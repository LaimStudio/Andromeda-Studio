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
        private static bool _autoHidden = true;

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
        
        public static async void VisibleAnimation(bool type)
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
                if (AutoHidden == false)
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
