using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndromedaApi.StaticComponents
{
    public class ProjectTemplate
    {
        public string Name;
        public ProjectFile[] Files;
    }

    public struct ProjectFile
    {
        public string Path;
        public string Content;
        public string File;
    }
}
