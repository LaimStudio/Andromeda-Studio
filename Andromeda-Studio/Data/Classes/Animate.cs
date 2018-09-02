using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace AndromedaStudio.Classes
{
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
            if (Database.Settings.Animation == false)
                time = 1;
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
            if (Database.Settings.Animation == false)
                time = 1;

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
}
