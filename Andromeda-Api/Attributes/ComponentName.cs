using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndromedaApi.Attributes
{
    public class ComponentName : Attribute
    {
        public string Name;

        public ComponentName(string name)
        {
            Name = name;
        }
    }
}
