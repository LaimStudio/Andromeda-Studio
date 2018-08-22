using System.Windows;
using System.Windows.Media;

namespace AndromedaStudio.Controls
{
    class ComboboxMenu : Button
    {
        #region Properties

        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        #region DependencyProperties

        public readonly static DependencyProperty DescriptionProperty =
             DependencyProperty.Register("Description", typeof(string),
             typeof(ComboboxMenu));

        #endregion

        #endregion
    }
}
