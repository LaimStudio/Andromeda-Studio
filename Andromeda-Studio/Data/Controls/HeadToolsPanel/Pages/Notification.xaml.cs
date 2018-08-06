using AndromedaStudio.Classes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AndromedaStudio.Controls.HeadToolsPanel.Pages
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
                Notifications.Children.Clear();
                foreach (Notifications.Notification item in Database.NotificationsManager.Notifications)
                {
                    item.Click += Close_Notification;
                    Notifications.Children.Add(item);
                    
                }
            }
        }

        public void Add(Notifications.Notification obj)
        {
            BeginAnimation(HeightProperty, null);
            Height = double.NaN;
            Animate.Opacity(NotificationBorder, 1, 300);
            NotificationBorder.Visibility = Visibility.Visible;
            Notifications.Children.Add(obj);
            obj.Click += Close_Notification;
            obj.Opacity = 0;
            Animate.Opacity(obj, 1, 300);
        }

        private void Close_Notification(object sender, RoutedEventArgs e)
        {
            var obj = (Notifications.Notification)sender;
            Database.NotificationsManager.Remove(obj);
        }

        async public void Cleared()
        {
            Animate.Opacity(NotificationBorder, 0, 300);
            await Animate.Size(this, Width, 130, 300);
        }

        private void Menu_Select(object sender, RoutedEventArgs e)
        {
            Classes.Menu.SetPage(sender);
            Classes.HeadTools.HideContent();
        }
    }
}
