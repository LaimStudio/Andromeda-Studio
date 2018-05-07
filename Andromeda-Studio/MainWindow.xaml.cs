using System.Windows;
using System.Windows.Input;

namespace AndromedaStudio
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region GUI

        private void WindowMove(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        #endregion
    }
}
