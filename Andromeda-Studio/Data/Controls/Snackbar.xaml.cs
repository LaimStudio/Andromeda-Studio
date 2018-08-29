using AndromedaStudio.Classes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AndromedaStudio.Controls
{
#pragma warning disable CS4014
    public partial class Snackbar : UserControl
    {
        public Snackbar() => InitializeComponent();

        private bool _lock = false;

        public async void Show(Notifications.Notification notification)
        {
            Panel parent = Database.MainWindow.ContentClip;
            if (parent.Children.Count >= 3)
                _lock = true;

            if (_lock)
                await WaitLock();
            
            foreach (FrameworkElement child in parent.Children)
            {
                if (child is Snackbar)
                {
                    Animate.Margin(child, new Thickness(25, 0, 0, child.Margin.Bottom + 60));
                    //Animate.Margin(child, new Thickness(0, child.Margin.Top + 60, 25, 0));
                }
            }

            _lock = true;

            var obj = new Notifications.Notification
            {
                Icon = notification.Icon,
                Content = notification.Content,
                Description = notification.Description,
            };

            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Bottom;
            //HorizontalAlignment = HorizontalAlignment.Right;
            //VerticalAlignment = VerticalAlignment.Top;

            Margin = new Thickness(25, 0, 0, -45);
            //Margin = new Thickness(0, -45, 25, 0);
            ContentPanel.Children.Add(obj);
            parent.Children.Add(this);

            await Animate.Margin(this, new Thickness(25, 0, 0, 25));
            //await Animate.Margin(this, new Thickness(0, 15, 25, 0));

            _lock = false;
            await Task.Delay(3000);

            _lock = true;
            if (Margin.Bottom > 25)
            {
                Animate.Opacity(this, 0);
                await Animate.Margin(this, new Thickness(25, 0, 0, Margin.Bottom + 25));
            }
            else
            {
                await Animate.Margin(this, new Thickness(25, 0, 0, -45));
            }

            //if (Margin.Top > 15)
            //{
            //    Animate.Opacity(this, 0);
            //    await Animate.Margin(this, new Thickness(0, Margin.Top + 25, 25, 0));
            //}
            //else
            //{
            //    await Animate.Margin(this, new Thickness(0, -45, 25, -45));
            //}

            parent.Children.Remove(this);
            _lock = false;
        }

        async Task WaitLock()
        {
            while (_lock)
            {
                await Task.Delay(1);
            }
        }
    }
}
