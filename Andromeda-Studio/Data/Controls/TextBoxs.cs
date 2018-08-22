using System.Windows;
using System.Windows.Media;

namespace AndromedaStudio.Controls
{
    class TextBox : System.Windows.Controls.TextBox
    {
        #region Properties
        
        public string PlaceHolder
        {
            get => (string)GetValue(PlaceHolderProperty);
            set => SetValue(PlaceHolderProperty, value);
        }

        public bool Necessarily
        {
            get => (bool)GetValue(NecessarilyProperty);
            set => SetValue(NecessarilyProperty, value);
        }

        public bool Error
        {
            get => (bool)GetValue(ErrorProperty);
            set => SetValue(ErrorProperty, value);
        }

        #region DependencyProperties

        public readonly static DependencyProperty PlaceHolderProperty =
            DependencyProperty.Register("PlaceHolder", typeof(string),
            typeof(TextBox));

        public readonly static DependencyProperty NecessarilyProperty =
            DependencyProperty.Register("Necessarily", typeof(bool),
            typeof(TextBox));

        public readonly static DependencyProperty ErrorProperty =
            DependencyProperty.Register("Error", typeof(bool),
            typeof(TextBox));

        #endregion

        #endregion
    }
}