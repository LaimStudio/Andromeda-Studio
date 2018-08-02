using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndromedaStudio.Data.Classes;

namespace AndromedaStudio.Data.Components
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
