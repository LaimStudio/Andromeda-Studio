using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Threading.Tasks;

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
                if(!value)
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
            if(type)
            {
                Animate.Opacity(window.ToolsList, 1, 300);
                await Animate.Opacity(window.ToolsCircles, 0);
                window.ToolsCircles.Children.Clear();
                _lock = false;

                if(!Visible)
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

                Database.MainWindow.ToolsCircles.Children.Add(new Separator { Margin = new Thickness(14,3,14,3), Opacity = 0.5 });
                
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
            var toolslist = (StackPanel)sender.Parent;
            int bottomArrow = 0;
            int count = 0;
            int bottomContent = 10;

            if (toolslist.Name != "ToolsList")
            {
                bottomArrow = 10;
            }
            else
            {
                count = toolslist.Children.IndexOf(sender);
                count = toolslist.Children.Count - count - 1;
                bottomArrow = 17 + 40 * count;
                if(bottomArrow > 140)
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
                tools.Arrow.Margin = new Thickness(0, 0, 0, bottomArrow + 5);
                tools.Visibility = Visibility.Visible;

                tools.Opacity = 0;
                await Animate.Opacity(tools, 1);
            }
            else
            {
                _toolChecked = sender;

                Animate.Margin(tools, new Thickness(0, 0, 55, bottomContent));
                await Animate.Margin(tools.Arrow, new Thickness(0, 0, 0, bottomArrow + 5));
            }
        }

        private static bool _lockHide = false;
        public static async void HideContent()
        {
            if (_lockHide)
                return;

            _lockHide = true;

            var tools = Database.Tools;

            _toolChecked.IsChecked = false;
            _toolChecked = null;
            AutoHidden = _autoHiddenSetting;

            await Animate.Opacity(tools, 0);
            tools.SetPage(null, 0);
            tools.Visibility = Visibility.Collapsed;

            _lockHide = false;
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

        public static async Task Margin(FrameworkElement sender, Thickness value, int time = 200, bool blur = false)
        {
            sender.BeginAnimation(FrameworkElement.MarginProperty, new ThicknessAnimation
            {
                To = value,
                AccelerationRatio = 0.2,
                DecelerationRatio = 0.7,
                Duration = TimeSpan.FromMilliseconds(time)
            });

            if(blur)
            {
                var effect = new System.Windows.Media.Effects.BlurEffect { Radius = 0 };
                var transform = new System.Windows.Media.ScaleTransform { ScaleY = 1 };

                sender.Effect = effect;
                sender.RenderTransform = transform;

                effect.BeginAnimation(System.Windows.Media.Effects.BlurEffect.RadiusProperty, new DoubleAnimation
                {
                    To = 5,
                    AccelerationRatio = 0.2,
                    DecelerationRatio = 0.7,
                    Duration = TimeSpan.FromMilliseconds(time / 2)
                });
                transform.BeginAnimation(System.Windows.Media.ScaleTransform.ScaleYProperty, new DoubleAnimation
                {
                    To = 1.5,
                    AccelerationRatio = 0.2,
                    DecelerationRatio = 0.7,
                    Duration = TimeSpan.FromMilliseconds(time / 2)
                });

                await Task.Delay(time / 2);

                effect.BeginAnimation(System.Windows.Media.Effects.BlurEffect.RadiusProperty, new DoubleAnimation
                {
                    To = 0,
                    AccelerationRatio = 0.2,
                    DecelerationRatio = 0.7,
                    Duration = TimeSpan.FromMilliseconds(time / 2 - 100)
                });
                transform.BeginAnimation(System.Windows.Media.ScaleTransform.ScaleYProperty, new DoubleAnimation
                {
                    To = 1,
                    AccelerationRatio = 0.2,
                    DecelerationRatio = 0.7,
                    Duration = TimeSpan.FromMilliseconds(time / 2 - 100)
                });

            }

            await Task.Delay(time);
            sender.Margin = value;
            sender.BeginAnimation(FrameworkElement.MarginProperty, null);
        }
    }
}
