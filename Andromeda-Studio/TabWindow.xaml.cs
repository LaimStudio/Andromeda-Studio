using AndromedaStudio.Classes;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AndromedaStudio
{
    public partial class TabWindow : Window
    {
        public TabWindow()
        {
            InitializeComponent();
        }

        #region GUI

        #region Window
        void Window_SourceInitialized(object sender, EventArgs e)
        {
            MaximizedToWSNone.Set(this);
        }
        #endregion

        private void WindowButtonsHandler(object sender, RoutedEventArgs e)
        {
            Classes.WindowButtonsHandler.Set(this, (FrameworkElement)sender);
        }

        #endregion
        
        private void TabControlSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var obj = (Dragablz.TabablzControl)sender;
            if (obj.Items.Count == 0)
            {
                Close();
            }
        }
    }
}
