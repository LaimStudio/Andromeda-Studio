using System.Windows;

namespace AndromedaStudio.Data.Classes
{
    class Tools
    {
        private static bool _autoHidden = true;
        private static bool _visible = false;

        public static bool AutoHidden
        {
            get => _autoHidden;
            set
            {
                _autoHidden = value;
                if(value == false && _visible == false)
                {
                    VisibleAnimation(true);
                }
            }
        }

        public static bool Visible
        {
            get => _visible;
            set
            {
                _visible = value;
                if(AutoHidden == true)
                {
                    VisibleAnimation(value);
                }
            }
        }

        private static void VisibleAnimation(bool type)
        {
            Database.MainWindow.ToolsCircles.Children.Clear();
            if(type)
            {
                Database.MainWindow.ToolsContent.Visibility = Visibility.Visible;
            }
            else
            {
                Database.MainWindow.ToolsContent.Visibility = Visibility.Hidden;
                int count = Database.MainWindow.ToolsContent.Children.Count;
                while (count != 0)
                {
                    count--;
                    Database.MainWindow.ToolsCircles.Children.Add(new Controls.RadioButton());
                }
            }
        }

    }
}
