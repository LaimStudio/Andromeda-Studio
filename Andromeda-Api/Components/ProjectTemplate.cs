using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndromedaApi.Components
{
    public abstract class ProjectTemplate
    {
        public virtual File[] GetStructure()
        {
            return new File[0];
        }
    }

    public struct File
    {
        public string Path;
        public string Content;

        public File(string path, string content)
        {
            Path = path;
            Content = content;
        }
    }
}
