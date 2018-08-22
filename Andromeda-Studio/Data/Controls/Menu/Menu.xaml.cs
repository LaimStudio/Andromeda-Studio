using AndromedaStudio.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AndromedaStudio.Controls
{
    public partial class Menu : UserControl
    {
        #pragma warning disable CS4014
        public Menu() => InitializeComponent();

        public string Page { get; private set; }

        private List<string> _pages = new List<string>(); 

        public async void SetPage(string value, string arg)
        {
            Page = value;
            PageName.Content = TryFindResource("@" + Page);

            if (value != null)
            {
                if (_pages.Count == 0)
                {
                    _pages.Add(value);
                    Frames.BeginAnimation(FrameworkElement.WidthProperty, null);
                    Frames.BeginAnimation(FrameworkElement.HeightProperty, null);
                    Frame.Margin = new Thickness(0);

                    Frame.NavigationService.Navigate(new Uri(@"Data\Controls\Menu\Pages\" + Page + ".xaml", UriKind.Relative));
                    await Task.Delay(1);

                    if (value == "Settings")
                    {
                        await Task.Delay(1);
                        var c = (MenuPages.Settings)Frame.Content;
                        c.SetPage(arg);
                    }

                    if (value == "SelectedDialog")
                    {
                        /*await Task.Delay(1);
                        var c = (MenuPages.Settings)Frame.Content;
                        c.SetPage(arg);*/
                    }

                    var content = (Page)Frame.Content;
                    Frames.Width = content.Width;
                    Frames.Height = content.Height;
                    return;
                }

                if (_pages.Count > 0)
                {
                    IsHitTestVisible = false;
                    _pages.Add(value);

                    Frame.Margin = new Thickness((Frame.ActualWidth / 2), 0, -(Frame.ActualWidth / 2), 0);
                    Frame.Opacity = 0;

                    var Frame2 = new Frame
                    {
                        Margin = new Thickness(0),
                        Content = Frame.Content,
                        Name = _pages.ElementAt(_pages.Count - 2),
                        
                        IsHitTestVisible = false
                    };
                    Frames.Children.Add(Frame2);

                    Frame.NavigationService.Navigate(new Uri(@"Data\Controls\Menu\Pages\" + Page + ".xaml", UriKind.Relative));
                    await Task.Delay(1);
                    var content = (Page)Frame.Content;

                    Animate.Opacity(Frame, 1, 200);
                    Animate.Opacity(Frame2, 0, 200);
                    Animate.Margin(Frame2, new Thickness(-(Frame.ActualWidth / 2), 0, (Frame.ActualWidth / 2), 0), 200);
                    Animate.Margin(Frame, new Thickness(0), 200);

                    await Animate.Size(Frames, content.Width, content.Height);
                    IsHitTestVisible = true;
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
            IsHitTestVisible = false;
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
                    Margin = new Thickness(0),
                    Content = Frame.Content
                };
                Frames.Children.Add(Frame2);

                Frame c = null;

                foreach(Frame i in Frames.Children)
                {
                    if(i.Name == Page)
                    {
                        c = i;
                    }
                }
                
                Frame.Content = c.Content;
                await Task.Delay(1);
                var content = (Page)Frame.Content;

                Animate.Opacity(Frame, 1, 200);
                Animate.Opacity(Frame2, 0, 200);
                Animate.Margin(Frame2, new Thickness((Frame.ActualWidth / 2), 0, -(Frame.ActualWidth / 2), 0), 200);
                Animate.Margin(Frame, new Thickness(0), 200);

                await Animate.Size(Frames, content.Width, content.Height);
                
                Frames.Children.Remove(Frame2);
            }
            IsHitTestVisible = true;
        }
    }
}
