using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndromedaApi.Components
{
    public class ProjectTemplate : Component
    {
        public List<File> Files;

        public class File
        {
            public string Path;
            public string Content;
        }
    }
}
