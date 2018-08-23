using System.Windows.Controls;
using System.Diagnostics;
using System.Windows;
using ICSharpCode.AvalonEdit.Highlighting;

namespace AndromedaStudio.Controls.MenuPages
{
    public partial class Debug : Page
    {
        private IHighlightingDefinition _highlighting;
        public Debug()
        {

            InitializeComponent();
            _highlighting = HighlightingManager.Instance.GetDefinitionByExtension(".py");
            DataContext = this;
        }
    }
}
