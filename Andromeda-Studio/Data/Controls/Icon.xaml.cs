using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AndromedaStudio.Controls
{
    /// <summary>
    /// EN: Represents a control that displays SVG icons.
    /// RU: Представляет элемент управления, отображающий SVG иконки.
    /// </summary>
    public partial class Icon : UserControl
    {
        public Icon() => InitializeComponent();

        #region Properties

        /// <summary>
        /// EN: Gets or sets the geometry value.
        /// RU: Получает или задает значение геометрии.
        /// </summary>
        public Geometry Data
        {
            get => (Geometry)GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

        /// <summary>
        /// EN: Gets or sets the System.Windows.Media.Brush
        /// RU: Получает или задает System.Windows.Media.Brush
        /// </summary>
        public Brush Fill
        {
            get => (Brush)GetValue(FillProperty);
            set => SetValue(FillProperty, value);
        }

        /// <summary>
        /// EN: Gets or sets the value of the external content field.
        /// RU: Получает или задает значение внешнего поля содержимого.
        /// </summary>
        public double Size
        {
            get => (double)GetValue(SizeProperty);
            set => SetValue(SizeProperty, value);
        }

        #region DependencyProperties

        public readonly static DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(Geometry),
            typeof(Icon));

        public readonly static DependencyProperty FillProperty =
            DependencyProperty.Register("Fill", typeof(Brush),
            typeof(Icon), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0,0,0))));

        public readonly static DependencyProperty SizeProperty =
            DependencyProperty.Register("Size", typeof(double),
            typeof(Icon), new UIPropertyMetadata(14.0));

        #endregion

        #endregion
    }
}
