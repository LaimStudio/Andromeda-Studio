using AndromedaStudio.Data.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AndromedaStudio.Data.Controls
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : UserControl
    {
        #pragma warning disable CS4014
        public Menu() => InitializeComponent();

        public string Page { get; private set; }

        private List<string> _pages = new List<string>(); 

        public async void SetPage(string value)
        {
            Page = value;
            PageName.Content = TryFindResource("@" + Page);

            if (value != null)
            {
                if (_pages.Count == 0)
                {
                    _pages.Add(value);
                    Frame.Margin = new Thickness(13, 0, 13, 0);
                    Frame.NavigationService.Navigate(new Uri(@"Data\Controls\Borders\Menu\Pages\" + Page + ".xaml", UriKind.Relative));
                    return;
                }

                if (_pages.Count > 0)
                {
                    _pages.Add(value);

                    Frame.Margin = new Thickness((Frame.ActualWidth / 2), 0, -(Frame.ActualWidth / 2), 0);
                    Frame.Opacity = 0;

                    var Frame2 = new Frame
                    {
                        Margin = new Thickness(13, 0, 13, 0),
                        Content = Frame.Content
                    };
                    Frames.Children.Add(Frame2);

                    Animate.Opacity(Frame, 1, 200);
                    Animate.Opacity(Frame2, 0, 200);
                    Animate.Margin(Frame2, new Thickness(-(Frame.ActualWidth / 2), 0, (Frame.ActualWidth / 2), 0), 200);
                    Frame.NavigationService.Navigate(new Uri(@"Data\Controls\Borders\Menu\Pages\" + Page + ".xaml", UriKind.Relative));
                    await Animate.Margin(Frame, new Thickness(13, 0, 13, 0), 200);
                    Frames.Children.Remove(Frame2);
                }
            }
            else
            {
                Frame.NavigationService.Navigate(null);
                _pages.Clear();
            }
        }

        private async void Back(object sender, RoutedEventArgs e)
        {

            if (_pages.Count == 1)
            {
                Classes.Menu.SetPage(null);
            }

            if (_pages.Count > 1)
            {
                _pages.RemoveAt(_pages.Count - 1);
                Page = _pages[_pages.Count - 1];
                PageName.Content = TryFindResource("@" + Page);

                Frame.Margin = new Thickness(-(Frame.ActualWidth / 2), 0, (Frame.ActualWidth / 2), 0);
                Frame.Opacity = 0;

                var Frame2 = new Frame
                {
                    Margin = new Thickness(13, 0, 13, 0),
                    Content = Frame.Content
                };
                Frames.Children.Add(Frame2);

                Animate.Opacity(Frame, 1, 200);
                Animate.Opacity(Frame2, 0, 200);
                Animate.Margin(Frame2, new Thickness((Frame.ActualWidth / 2), 0, -(Frame.ActualWidth / 2), 0), 200);
                Frame.NavigationService.Navigate(new Uri(@"Data\Controls\Borders\Menu\Pages\" + Page + ".xaml", UriKind.Relative));
                await Animate.Margin(Frame, new Thickness(13, 0, 13, 0), 200);
                Frames.Children.Remove(Frame2);
            }
        }
    }
}
