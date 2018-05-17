using System;
using System.Windows.Controls;

namespace AndromedaStudio.Data.Controls
{
    public partial class Tools : UserControl
    {
        public Tools() => InitializeComponent();

        private string _pageName;

        public string Page
        {
            get => _pageName;
            set
            {
                _pageName = value;
                PageName.Content = TryFindResource("@"+value);

                if (value != null)
                {
                    Frame.NavigationService.Navigate(new Uri(@"Data\Controls\ToolsPanel\Pages\" + _pageName + ".xaml", UriKind.Relative));
                }
                else
                {
                    Frame.NavigationService.Navigate(null);
                }

            }
        }
    }
}
