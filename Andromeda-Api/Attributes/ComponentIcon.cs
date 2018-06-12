using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndromedaApi.Attributes
{
    public class ComponentIcon : Attribute
    {
        public string Path;

        public ComponentIcon(string path)
        {
            Path = path;
        }
    }
}
