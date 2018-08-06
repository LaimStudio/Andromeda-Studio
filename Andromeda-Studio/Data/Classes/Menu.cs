using System.Windows;

namespace AndromedaStudio.Classes
{
    #pragma warning disable CS4014

    class Menu
    {
        public static bool IsOpened
        {
            get;
            private set;
        }

        public static void SetPage(object obj)
        {
            if (obj == null)
            {
                VisibleAnimation(false);
                IsOpened = false;
                return;
            }

            var menu = Database.Menu;
            var sender = (FrameworkElement)obj;
            var value = (string)sender.Tag;
            string arg = null;

            if (value.IndexOf(".") != -1)
            {
                string[] values = value.Split('.');
                value = values[0];
                arg = values[1];
            }

            menu.SetPage(value, arg);

            if (!IsOpened)
            {
                VisibleAnimation(true);
                IsOpened = true;
            }
        }

        private static async void VisibleAnimation(bool type)
        {
            var menu = Database.Menu;
            var mainWindow = Database.MainWindow;
            if (type)
            {
                menu.Opacity = 0;
                menu.Visibility = Visibility.Visible;
                Animate.Opacity(menu, 1);
                Animate.Opacity(mainWindow.WindowContent, 0.5);
                mainWindow.WindowContent.IsHitTestVisible = false;
            }
            else
            {
                menu.Opacity = 1;
                Animate.Opacity(mainWindow.WindowContent, 1);
                await Animate.Opacity(menu, 0);
                menu.SetPage(null, null);
                menu.Visibility = Visibility.Collapsed;
                mainWindow.WindowContent.IsHitTestVisible = true;
            }
        }
    }
}
