using AndromedaStudio.Data.Classes;
using System.Windows;
using System.Windows.Controls;

namespace AndromedaStudio.Data.Controls.HeadToolsPanel.Pages
{
    #pragma warning disable CS4014
    public partial class Notification : Page
    {
        public Notification()
        {
            InitializeComponent();

            if(Database.NotificationsManager.Notifications.Count > 0)
            {
                Height = double.NaN;
                NotificationBorder.Visibility = Visibility.Visible;
                Database.MainWindow.ProfileButton.New = false;
                foreach (Notifications.Notification item in Database.NotificationsManager.Notifications)
                {
                    var obj = new Controls.Notification
                    {
                        Content = item.Caption,
                        Description = item.Description,
                        Icon = item.Icon
                    };
                    obj.Click += Close_Notification;
                    Notifications.Children.Add(obj);
                }
            }
        }

        private async void Close_Notification(object sender, RoutedEventArgs e)
        {
            var obj = (Controls.Notification)sender;
            //Database.NotificationsManager.Remove(null, (byte)Notifications.Children.IndexOf(obj));
            Animate.Opacity(obj, 0, 300);
            await Animate.Size(obj, 0, 0, 300);
            Notifications.Children.Remove(obj);
        }

        private void Menu_Select(object sender, RoutedEventArgs e)
        {
            Classes.Menu.SetPage(sender);
            Classes.HeadTools.HideContent();
        }
    }
}
