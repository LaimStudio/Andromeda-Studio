using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AndromedaStudio.Data.Controls
{
    class ScrollViewer : System.Windows.Controls.ScrollViewer
    {
        public ScrollViewer() => PreviewMouseWheel += new MouseWheelEventHandler(sMouseWheel);

        double scrollspeed = 0;
        bool arrow;
        async private void sMouseWheel(object sender, MouseWheelEventArgs e)
        {
            var obj = (ScrollViewer)sender;
            obj.ScrollToVerticalOffset(obj.VerticalOffset);

            if (e.Delta > 0)
                arrow = true;
            else
                arrow = false;

            if (scrollspeed > 0)
            {
                if (scrollspeed < 25)
                {
                    scrollspeed += 5;
                }
            }
            else
            {
                double i = 0.6;
                for (scrollspeed = 10; scrollspeed > 0; scrollspeed = scrollspeed - i)
                {
                    if (scrollspeed > 10)
                    {
                        i += 0.07;
                    }
                    if (arrow)
                    {
                        obj.ScrollToVerticalOffset(obj.VerticalOffset - scrollspeed);
                    }
                    else
                    {
                        obj.ScrollToVerticalOffset(obj.VerticalOffset + scrollspeed);
                    }
                    await Task.Delay(1);
                }
            }
        }
    }
}
