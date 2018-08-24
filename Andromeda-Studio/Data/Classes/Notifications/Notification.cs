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

        public double ProgressValue
        {
            get => (double)GetValue(ProgressValueProperty);
            set => SetValue(ProgressValueProperty, value);
        }

        #region DependencyProperties

        public readonly static DependencyProperty DescriptionProperty =
             DependencyProperty.Register("Description", typeof(string),
             typeof(Notification));

        public readonly static DependencyProperty ProgressValueProperty =
             DependencyProperty.Register("ProgressValue", typeof(double),
             typeof(Notification), new PropertyMetadata(0.0));

        #endregion

        #endregion
    }
}
