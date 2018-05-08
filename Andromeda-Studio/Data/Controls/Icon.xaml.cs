using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AndromedaStudio.Data.Controls
{
    /// <summary>
    /// EN: Represents a control that displays SVG icons.
    /// RU: Представляет элемент управления, отображающий SVG иконки.
    /// </summary>
    public partial class Icon : UserControl
    {
        public Icon()
        {
            InitializeComponent();
        }

        #region Properties

        /// <summary>
        /// EN: Gets or sets the geometry value.
        /// RU: Получает или задает значение геометрии.
        /// </summary>
        public Geometry Data
        {
            get => Container.Data;
            set => Container.Data = value;
        }

        /// <summary>
        /// EN: Gets or sets the System.Windows.Media.Brush
        /// RU: Получает или задает System.Windows.Media.Brush
        /// </summary>
        public Brush Fill
        {
            get => Container.Fill;
            set => Container.Fill = value;
        }

        /// <summary>
        /// EN: Gets or sets the value of the external content field.
        /// RU: Получает или задает значение внешнего поля содержимого.
        /// </summary>
        public Thickness IconMargin
        {
            get => Container.Margin;
            set => Container.Margin = value;
        }

        #endregion
    }
}
