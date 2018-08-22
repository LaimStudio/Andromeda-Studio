using System.Collections.Generic;
using AndromedaStudio.Classes;

namespace AndromedaStudio.Components
{
    public class ProjectTemplate : Component
    {
        public List<File> Files;

        public class File
        {
            public string Path { get; set; }
            public string Content { get; set; }
        }
    }
}
