using System.Windows.Controls;
using ICSharpCode.AvalonEdit.Highlighting;
using System.Xml;
using System.IO;
using System.Reflection;

namespace AndromedaStudio.Controls.MenuPages
{
    public partial class Debug : Page
    {
        public Debug()
        {
            IHighlightingDefinition customHighlighting;
            using (Stream s = Assembly.GetExecutingAssembly().GetManifestResourceStream("AndromedaStudio.Data.Themes.Highlightings.Day.Python.xshd"))
            {
                using (XmlReader reader = new XmlTextReader(s))
                {
                    customHighlighting = ICSharpCode.AvalonEdit.Highlighting.Xshd.
                        HighlightingLoader.Load(reader, HighlightingManager.Instance);
                }
            }

            HighlightingManager.Instance.RegisterHighlighting("Python", new string[] { ".cool" }, customHighlighting);
            InitializeComponent();
        }
    }
}
