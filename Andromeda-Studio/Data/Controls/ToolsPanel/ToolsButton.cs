using AndromedaStudio.Classes;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AndromedaStudio.Controls
{
    class ToolsButton : RadioButton
    {
        public ToolsButton()
        {
            Click += Tools_Selected;
        }

        private void Tools_Selected(object sender, RoutedEventArgs e)
        {
            var obj = (RadioButton)e.Source;
            if (Database.Tools.IsHitTestVisible == false || (Classes.Tools.IsOpened == true && obj.IsChecked == false))
            {
                Classes.Tools.HideContent();
                obj.IsChecked = false;
                return;
            }
            
            if (obj.IsChecked == true)
                Classes.Tools.SetPage(obj);
            else
                Classes.Tools.HideContent();
        }
    }
}