using AndromedaStudio.Classes;
using System.Collections.Generic;

namespace AndromedaStudio.Notifications
{
    #pragma warning disable CS4014

    public class Manager
    {
        public List<Notification> Notifications = new List<Notification>();

        public void Add(Notification obj)
        {
            Notifications.Insert(0, obj);
            if (!(Database.HeadTools.Page == "Notification" && HeadTools.IsOpened))
                Database.MainWindow.ProfileButton.New = true;
            Database.NotificationsPanel.Add(obj);
        }

        async public void Remove(Notification obj)
        {
            Animate.Opacity(obj, 0, 300);
            Notifications.Remove(obj);
            if (Notifications.Count == 0)
            {
                Database.MainWindow.ProfileButton.New = false;
                Database.NotificationsPanel.Cleared();
            }

            await Animate.Size(obj, 0, 0, 300);
            Database.NotificationsPanel.Notifications.Children.Remove(obj);
        }
    }
}
