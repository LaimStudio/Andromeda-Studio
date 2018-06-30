using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AndromedaStudio.Data.Controls.HeadToolsPanel.Pages
{
    public partial class Notification : Page
    {
        public Notification()
        {
            InitializeComponent();

            if(Database.NotificationsManager.Notifications.Count > 0)
            {
                NotificationBorder.Visibility = Visibility.Visible;
                Database.MainWindow.NotificationButton.New = false;
                foreach (Notifications.Notification item in Database.NotificationsManager.Notifications)
                {
                    var obj = new Controls.Notification
                    {
                        Content = item.Caption,
                        Description = item.Description,
                        Icon = item.Icon
                    };
                    Notifications.Children.Add(obj);
                }
            }

            if (Database.NotificationsManager.Notifications.Count > 2)
            {
                Height = 200;
            }
        }

        private void Menu_Select(object sender, RoutedEventArgs e)
        {
            Data.Classes.Menu.SetPage(sender);
            Classes.HeadTools.HideContent();
        }
    }
}
