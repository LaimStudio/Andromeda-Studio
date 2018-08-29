using System.Windows;

namespace AndromedaStudio.Classes
{
    public class WindowButtonsHandler
    {
        public static void Set(Window window, FrameworkElement obj)
        {
            switch (obj.Tag)
            {
                case "Close":
                    window.Close();
                    break;

                case "Maximize":
                    if (window.WindowState == WindowState.Normal)
                        window.WindowState = WindowState.Maximized;
                    else
                        window.WindowState = WindowState.Normal;
                    break;

                case "Minimize":
                    window.WindowState = WindowState.Minimized;
                    break;
            }
        }
    }
}
