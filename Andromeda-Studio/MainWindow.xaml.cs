using AndromedaStudio.Data.Classes;
using System.Windows.Input;
using System.Windows;
using System.Windows.Controls;

namespace AndromedaStudio
{
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();

        #region GUI

        private void WindowButtonsHandler(object sender, RoutedEventArgs e)
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

        #region Tools

        private void Tools_MouseEnter(object sender, MouseEventArgs e)
        {
            Tools.Visible = true;
        }

        private void Tools_MouseLeave(object sender, MouseEventArgs e)
        {
            Tools.Visible = false;
        }

        private void Tools_Selected(object sender, RoutedEventArgs e)
        {
            var obj = (RadioButton)e.Source;
            if (obj.IsChecked == true)
                Tools.SetPage(obj);
            else
                Tools.HideContent();
        }

        private void ContentFocus(object sender, MouseButtonEventArgs e)
        {
            if(Tools.IsOpened)
            {
                Tools.HideContent();
                Tools.Visible = false;
            }
        }

        #endregion
    }
}
