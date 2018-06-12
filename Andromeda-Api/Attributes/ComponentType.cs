using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndromedaApi.Attributes
{
    public class ComponentType : Attribute
    {
        public string Type;

        public ComponentType(string type)
        {
            Type = type;
        }
    }
}
