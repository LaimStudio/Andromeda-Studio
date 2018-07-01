using System.Collections.Generic;
using System.IO;

namespace AndromedaApi
{
    public class Package
    {
        public string Name;
        public string Version;
        public string Author;

        public List<Component> Components = new List<Component>();

        public Package(Manifest manifest, string path)
        {
            Name = manifest.Name;
            Version = manifest.Version;
            Author = manifest.Author;

            foreach (var component in manifest.Components)
                Components.Add(Component.Load(Path.Combine(path, component)));
        }
    }
}