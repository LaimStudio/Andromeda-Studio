using AndromedaStudio.Data.Classes;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AndromedaStudio.Data.Controls
{
    public partial class Tools : UserControl
    {
        #pragma warning disable CS4014
        public Tools() => InitializeComponent();

        public string Page { get; private set; }

        private sbyte _pagenumber = 0;

        public async void SetPage(string value, sbyte number)
        {
            Page = value;
            PageName.Content = TryFindResource("@" + value);

            if (value != null)
            {
                if (_pagenumber == 0)
                {
                    Frame.Margin = new Thickness(13, 0, 13, 0);
                    Frame.NavigationService.Navigate(new Uri(@"Data\Controls\ToolsPanel\Pages\" + Page + ".xaml", UriKind.Relative));

                    _pagenumber = number;
                    if (number == 0)
                    {
                        _pagenumber = -1;
                    }

                    return;
                }

                if (number < _pagenumber || number == 0)
                {
                    Frame.Margin = new Thickness(13, (Frame.ActualHeight / 2), 13, 0);
                    Frame.Opacity = 0;

                    var Frame2 = new Frame {
                        Margin = new Thickness(13, 0, 13, 0),
                        Content = Frame.Content
                    };
                    Frames.Children.Add(Frame2);

                    Animate.Opacity(Frame, 1, 200);
                    Animate.Opacity(Frame2, 0, 200);
                    Animate.Margin(Frame2, new Thickness(13, -(Frame.ActualHeight / 2), 13, 0), 200);
                    Frame.NavigationService.Navigate(new Uri(@"Data\Controls\ToolsPanel\Pages\" + Page + ".xaml", UriKind.Relative));
                    await Animate.Margin(Frame, new Thickness(13, 0, 13, 0), 200);
                    Frames.Children.Remove(Frame2);
                }

                if (number > _pagenumber)
                {
                    Frame.Margin = new Thickness(13, -(Frame.ActualHeight / 2), 13, 0);
                    Frame.Opacity = 0;

                    var Frame2 = new Frame
                    {
                        Margin = new Thickness(13, 0, 13, 0),
                        Content = Frame.Content
                    };
                    Frames.Children.Add(Frame2);

                    Animate.Opacity(Frame, 1, 200);
                    Animate.Opacity(Frame2, 0, 200);
                    Animate.Margin(Frame2, new Thickness(13, (Frame.ActualHeight / 2), 13, 0), 200);
                    Frame.NavigationService.Navigate(new Uri(@"Data\Controls\ToolsPanel\Pages\" + Page + ".xaml", UriKind.Relative));
                    await Animate.Margin(Frame, new Thickness(13, 0, 13, 0), 200);
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

    class ToolsButton : RadioButton
    {
        public ToolsButton()
        {
            Click += Tools_Selected;
        }

        private void Tools_Selected(object sender, RoutedEventArgs e)
        {
            var obj = (RadioButton)e.Source;
            if (obj.IsChecked == true)
                Classes.Tools.SetPage(obj);
            else
                Classes.Tools.HideContent();
        }
    }
}
