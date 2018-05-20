using AndromedaStudio.Data.Classes;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AndromedaStudio.Data.Controls
{
#pragma warning disable CS4014
    public partial class Snackbar : UserControl
    {
        public Snackbar() => InitializeComponent();

        #region Properties
        
        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

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
             typeof(Snackbar));

        public readonly static DependencyProperty IconFillProperty =
            DependencyProperty.Register("IconFill", typeof(Brush),
            typeof(Snackbar), new PropertyMetadata(new SolidColorBrush(Color.FromRgb(0, 0, 0))));

        public readonly static DependencyProperty IconSizeProperty =
            DependencyProperty.Register("IconSize", typeof(double),
            typeof(Snackbar), new UIPropertyMetadata(14.0));

        public readonly static DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string),
            typeof(Snackbar));

        #endregion

        #endregion

        private bool _lock = false;

        public async void Show(Panel parent)
        {
            if (_lock)
                await WaitLock();

            foreach (FrameworkElement child in parent.Children)
            {
                if (child is Snackbar)
                {
                    Animate.Margin(child, new Thickness(25, 0, 0, child.Margin.Bottom + 50));
                }
            }

            _lock = true;
            
            if(Icon != null)
            {
                IconContent.Visibility = Visibility.Visible;
            }

            HorizontalAlignment = HorizontalAlignment.Left;
            VerticalAlignment = VerticalAlignment.Bottom;

            Margin = new Thickness(25, 0, 0, -45);
            parent.Children.Add(this);

            await Animate.Margin(this, new Thickness(25, 0, 0, 25));

            _lock = false;
            await Task.Delay(3000);

            _lock = true;
            if (Margin.Bottom > 25)
            {
                Animate.Opacity(this, 0);
                await Animate.Margin(this, new Thickness(25, 0, 0, Margin.Bottom + 25));
            }
            else
            {
                await Animate.Margin(this, new Thickness(25, 0, 0, -45));
            }
            parent.Children.Remove(this);
            _lock = false;
        }

        async Task WaitLock()
        {
            while (_lock)
            {
                await Task.Delay(1);
            }
        }
    }
}
