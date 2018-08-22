using System.Windows;

namespace AndromedaStudio.Notifications
{
    public class Notification : Controls.Button
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
             typeof(Notification));

        #endregion

        #endregion
    }
}
