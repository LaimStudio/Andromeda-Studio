using AndromedaStudio.Classes;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AndromedaStudio.Controls
{
    public partial class HeadTools : UserControl
    {
        #pragma warning disable CS4014
        public HeadTools() => InitializeComponent();

        public string Page { get; private set; }

        private sbyte _pagenumber = 0;

        public async void SetPage(string value, sbyte number)
        {
            Page = value;

            if (value != null)
            {
                if (_pagenumber == 0)
                {
                    Frame.Margin = new Thickness(0);
                    if(value == "Notification")
                    {
                        Frame.NavigationService.Content = Database.NotificationsPanel;
                        Database.MainWindow.ProfileButton.New = false;
                    }
                    else
                    {
                        Frame.NavigationService.Navigate(new Uri(@"Data\Controls\HeadToolsPanel\Pages\" + Page + ".xaml", UriKind.Relative));
                        await Task.Delay(1000);

                        _pagenumber = number;
                        if (number == 0)
                        {
                            _pagenumber = -1;
                        }

                        var content = (Page)Frame.Content;
                        Frames.Width = content.Width;
                        Frames.Height = content.Height;
                    }
                    return;
                }

                if (number < _pagenumber || number == 0 || number > _pagenumber)
                {
                    Thickness MarginStart, MarginFinish = new Thickness();
                    if (number > _pagenumber)
                    {
                        MarginStart = new Thickness(-(Frame.ActualWidth / 2), 0, (Frame.ActualWidth / 2), 0);
                        MarginFinish = new Thickness((Frame.ActualWidth / 2), 0, -(Frame.ActualWidth / 2), 0);
                    }
                    else
                    {
                        MarginStart = new Thickness((Frame.ActualWidth / 2), 0, -(Frame.ActualWidth / 2), 0);
                        MarginFinish = new Thickness(-(Frame.ActualWidth / 2), 0, (Frame.ActualWidth / 2), 0);
                    }

                    Frame.Margin = MarginStart;
                    Frame.Opacity = 0;

                    var Frame2 = new Frame
                    {
                        Margin = new Thickness(0),
                        Content = Frame.Content
                    };
                    Frames.Children.Add(Frame2);

                    Frame.NavigationService.Navigate(new Uri(@"Data\Controls\HeadToolsPanel\Pages\" + Page + ".xaml", UriKind.Relative));
                    await Task.Delay(1);
                    var content = (Page)Frame.Content;

                    Animate.Opacity(Frame, 1, 200);
                    Animate.Opacity(Frame2, 0, 200);
                    Animate.Margin(Frame2, MarginFinish, 200);
                    Animate.Margin(Frame, new Thickness(0), 200);
                    await Animate.Size(Frames, content.Width, content.Height);
                    Frames.Children.Remove(Frame2);
                }

                _pagenumber = number;
                if (number == 0)
                {
                    _pagenumber = -1;
                }
            }
            else
            {
                Frame.NavigationService.Navigate(null);
                _pagenumber = 0;
            }
        }
    }
}
