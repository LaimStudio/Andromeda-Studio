using AndromedaStudio.Classes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AndromedaStudio.Controls.MenuPages.Setting
{
    public partial class General : Page
    {
        public General()
        {
            InitializeComponent();
        }

        private void Menu_Select(object sender, RoutedEventArgs e)
        {
            Classes.Menu.SetPage(sender);
        }

        async private void Checkbox(object sender, RoutedEventArgs e)
        {
            var obj = (CheckBox)sender;
            var par = (string)obj.Tag;
            if (par == "NightTheme")
            {
                RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)Database.MainWindow.Body.ActualWidth, (int)Database.MainWindow.Body.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                renderTargetBitmap.Render(Database.MainWindow.Body);
                var img = new Image { Source = renderTargetBitmap };
                Database.MainWindow.Body.Children.Add(img);

                if (obj.IsChecked == true)
                {
                    App.Theme = "Night";
                }
                else
                {
                    App.Theme = "Day";
                }

                await Animate.Opacity(img, 0);
                Database.MainWindow.Body.Children.Remove(img);

                Database.Settings.Theme = App.Theme;
            }

            {
                {
                }
            }

        }
    }
}
