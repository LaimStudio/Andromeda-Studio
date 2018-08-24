using System.Windows.Controls;
using ICSharpCode.AvalonEdit.Highlighting;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Windows;
using AndromedaStudio.Classes;

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

        public async void Execute(object sender, RoutedEventArgs e)
        {
            await Database.PackageLoader.Execute(CodeTextEditor.Text);
            Classes.Menu.ClosePage();
        }
    }
}
