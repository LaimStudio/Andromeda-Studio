using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AndromedaStudio.Data.Controls.Designer
{
    [ContentProperty(nameof(Children))]  // Prior to C# 6.0, replace nameof(Children) with "Children"
    public partial class Constructor : UserControl
    {
        public static readonly DependencyPropertyKey ChildrenProperty = DependencyProperty.RegisterReadOnly(
            nameof(Children),  // Prior to C# 6.0, replace nameof(Children) with "Children"
            typeof(UIElementCollection),
            typeof(Constructor),
            new PropertyMetadata());

        public UIElementCollection Children
        {
            get => (UIElementCollection)GetValue(ChildrenProperty.DependencyProperty);
            private set {
                SetValue(ChildrenProperty, value);
            }
        }

        public Constructor()
        {
            InitializeComponent();
            Children = PART_Host.Children;
        }
    }
}
