using AndromedaStudio.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndromedaStudio.Classes.PackageApi
{
    public class Notifications
    {
        public void Show(string title, string message = "")
        {
            Database.MainWindow.Dispatcher.Invoke(() =>
            {
                Database.NotificationsManager.Add(new Notification
                {
                    Content = title,
                    Description = message
                });
            });
        }
    }
}
