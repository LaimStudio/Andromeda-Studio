using System.Windows.Media;
using AndromedaStudio.Controls.TabControl;

namespace AndromedaStudio.Controls
{
    class TabItem : System.Windows.Controls.TabItem
    {
        public TabItem() => Header = new TabHeader();

        #region Properties

        public Geometry Icon
        {
            set => ((TabHeader)Header).Icon.Data = value;
        }

        public string Title
        {
            set => ((TabHeader)Header).Title.Content = value;
        }

        public string Path;
        public int Hash;

        #endregion
    }
}
