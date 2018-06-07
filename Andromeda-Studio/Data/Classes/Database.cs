namespace AndromedaStudio.Data.Classes
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
        public static Controls.Tools Tools = new Controls.Tools {
            Visibility = System.Windows.Visibility.Collapsed,
            HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
            VerticalAlignment = System.Windows.VerticalAlignment.Bottom
        };
    }
}
