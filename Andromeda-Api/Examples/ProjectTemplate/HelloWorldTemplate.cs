using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndromedaApi.Components;

namespace AndromedaApi.Examples.ProjectTemplate
{
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
