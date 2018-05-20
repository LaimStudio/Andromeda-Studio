using System.Windows;
using System.Windows.Controls;

namespace AndromedaStudio.Data.Controls
{
    public partial class Snackbar : UserControl
    {
        public Snackbar() => InitializeComponent();

        #region Properties
        
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
        
        #region DependencyProperties

        public readonly static DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string),
            typeof(Snackbar));

        #endregion

        #endregion
    }
}
