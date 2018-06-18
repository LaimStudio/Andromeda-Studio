using AndromedaStudio.Data.Classes;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AndromedaStudio.Data.Controls.MenuPages
{
    public partial class Settings : Page
    {
        #pragma warning disable CS4014
        public Settings() => InitializeComponent();

        private sbyte _pagenumber = 0;

        public async void SetPage(string value)
        {
            sbyte number = 0;
            foreach (FrameworkElement obj in SettingsList.Items)
            {
                if((string)obj.Tag == value)
                {
                    number = (sbyte)SettingsList.Items.IndexOf(obj);
                    SettingsList.SelectedIndex = number;
                    break;
                }
            }

            if (_pagenumber == 0)
            {
                Frame.Margin = new Thickness(13, 0, 13, 0);
                Frame.NavigationService.Navigate(new Uri(@"Data\Controls\Menu\Pages\Settings\" + value + ".xaml", UriKind.Relative));

                _pagenumber = number;
                if (number == 0)
                {
                    _pagenumber = -1;
                }
                return;
            }

            if (number > _pagenumber)
            {
                Frame.Margin = new Thickness(13, (Frame.ActualHeight / 2), 13, 0);
                Frame.Opacity = 0;

                var Frame2 = new Frame
                {
                    Margin = new Thickness(13, 0, 13, 0),
                    Content = Frame.Content
                };
                Frames.Children.Add(Frame2);

                Animate.Opacity(Frame, 1, 200);
                Animate.Opacity(Frame2, 0, 200);
                Animate.Margin(Frame2, new Thickness(13, -(Frame.ActualHeight / 2), 13, 0), 200);
                Frame.NavigationService.Navigate(new Uri(@"Data\Controls\Menu\Pages\Settings\" + value + ".xaml", UriKind.Relative));
                await Animate.Margin(Frame, new Thickness(13, 0, 13, 0), 200);
                Frames.Children.Remove(Frame2);
            }

            if (number < _pagenumber)
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
                Frame.NavigationService.Navigate(new Uri(@"Data\Controls\Menu\Pages\Settings\" + value + ".xaml", UriKind.Relative));
                await Animate.Margin(Frame, new Thickness(13, 0, 13, 0), 200);
                Frames.Children.Remove(Frame2);
            }

            _pagenumber = number;
            if (number == 0)
            {
                _pagenumber = -1;
            }
        }

        private void PageSelect(object sender, RoutedEventArgs e)
        {
            var obj = (ListBoxItem)sender;
            SetPage((string)obj.Tag);
        }
    }
}
