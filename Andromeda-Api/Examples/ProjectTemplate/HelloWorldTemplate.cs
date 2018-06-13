using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndromedaApi.Components;
using AndromedaApi.Attributes;

namespace AndromedaApi.Examples.ProjectTemplate
{
    [ComponentName("HelloWorldTemplate")]
    [ComponentType("ProjectTemplate")]
    public class HelloWorldTemplate : Components.ProjectTemplate
    {
        public override File[] GetStructure()
        {
            return new File[]
            {
                new File("/main.php", "echo \"Hello world\"")
            };
        }
    }
}
