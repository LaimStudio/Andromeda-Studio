using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndromedaApi.Attributes
{
    public class ResourceName : Attribute
    {
        public string Name;

        public ResourceName(string name)
        {
            Name = name;
        }
    }
}
