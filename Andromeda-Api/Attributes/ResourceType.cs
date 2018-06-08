using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndromedaApi.Attributes
{
    public class ResourceType : Attribute
    {
        public string Type;

        public ResourceType(string type)
        {
            Type = type;
        }
    }
}
