namespace AndromedaStudio.Classes
{
    class Database
    {
        public static MainWindow MainWindow = new MainWindow();
        public static Controls.Menu Menu = new Controls.Menu
        {
            Visibility = System.Windows.Visibility.Collapsed,
            HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
            VerticalAlignment = System.Windows.VerticalAlignment.Center
        };
        public static Controls.Tools Tools = new Controls.Tools
        {
            Visibility = System.Windows.Visibility.Hidden,
            HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
            VerticalAlignment = System.Windows.VerticalAlignment.Bottom
        };
        public static Controls.HeadTools HeadTools = new Controls.HeadTools
        {
            Visibility = System.Windows.Visibility.Hidden,
            HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
            VerticalAlignment = System.Windows.VerticalAlignment.Top
        };

        public static Notifications.Manager NotificationsManager = new Notifications.Manager();

        public static Controls.HeadToolsPanel.Pages.Notification NotificationsPanel = new Controls.HeadToolsPanel.Pages.Notification();
    }
}
