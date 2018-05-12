using System.Windows;

namespace AndromedaStudio
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region GUI

        private void WindowButtons(object sender, RoutedEventArgs e)
        {
            var obj = (FrameworkElement)sender;
            switch(obj.Tag)
            {
                case "Close":
                    Application.Current.Shutdown();
                    break;

                case "Maximize":
                    WindowState = WindowState.Maximized;
                    obj.Tag = "Restore";
                    break;

                case "Minimize":
                    WindowState = WindowState.Minimized;
                    break;

                case "Restore":
                    WindowState = WindowState.Normal;
                    obj.Tag = "Maximize";
                    break;
            }
        }

        #endregion
    }
}
