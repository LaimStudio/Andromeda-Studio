using System.Windows;

namespace AndromedaStudio
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

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
                    if (WindowState == WindowState.Normal)
                        WindowState = WindowState.Maximized;
                    else
                        WindowState = WindowState.Normal;
                    break;

                case "Minimize":
                    WindowState = WindowState.Minimized;
                    break;
            }
        }

        #endregion

    }
}
