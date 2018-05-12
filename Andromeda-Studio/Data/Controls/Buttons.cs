using System.Windows;
using System.Windows.Media;

namespace AndromedaStudio.Data.Controls
{
    class Button : System.Windows.Controls.Button
    {
        #region Properties

        /// <summary>
        /// EN: Gets or sets the geometry value.
        /// RU: Получает или задает значение геометрии.
        /// </summary>
        public Geometry Icon
        {
            get => (Geometry)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        /// <summary>
        /// EN: Gets or sets the System.Windows.Media.Brush
        /// RU: Получает или задает System.Windows.Media.Brush
        /// </summary>
        public Brush IconFill
        {
            get => (Brush)GetValue(IconFillProperty);
            set => SetValue(IconFillProperty, value);
        }

        /// <summary>
        /// EN: Gets or sets the value of the external content field.
        /// RU: Получает или задает значение внешнего поля содержимого.
        /// </summary>
        public double IconSize
        {
            get => (double)GetValue(IconSizeProperty);
            set => SetValue(IconSizeProperty, value);
        }

        #region DependencyProperties

        public readonly static DependencyProperty IconProperty =
             DependencyProperty.Register("Icon", typeof(Geometry),
             typeof(Button));

        public readonly static DependencyProperty IconFillProperty =
            DependencyProperty.Register("IconFill", typeof(Brush),
            typeof(Button), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0, 0, 0))));

        public readonly static DependencyProperty IconSizeProperty =
            DependencyProperty.Register("IconSize", typeof(double),
            typeof(Button), new UIPropertyMetadata(16.0));

        #endregion

        #endregion
    }

    class TextButton : Button
    {
    }
}