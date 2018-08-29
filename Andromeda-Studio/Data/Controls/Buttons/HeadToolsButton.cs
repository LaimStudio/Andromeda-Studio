using AndromedaStudio.Classes;
using System.Windows;

namespace AndromedaStudio.Controls
{
    class HeadToolsButton : RadioButton
    {
        public HeadToolsButton()
        {
            Click += Tools_Selected;
        }

        private void Tools_Selected(object sender, RoutedEventArgs e)
        {
            var obj = (RadioButton)e.Source;
            if (Database.HeadTools.IsHitTestVisible == false || (Classes.HeadTools.IsOpened == true && obj.IsChecked == false))
            {
                Classes.HeadTools.HideContent();
                obj.IsChecked = false;
                return;
            }

            if (obj.IsChecked == true)
                Classes.HeadTools.SetPage(obj);
            else
                Classes.HeadTools.HideContent();
        }
    }
}
