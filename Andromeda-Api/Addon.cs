using System.Collections.Generic;
using System.IO;

namespace AndromedaApi
{
    public class Addon
    {
        public string Name;
        public string Version;

        public List<Component> Components = new List<Component>();

        public Addon(Manifest manifest, string path)
        {
            Name = manifest.Name;
            Version = manifest.Version;

            foreach (var component in manifest.Components)
                Components.Add(Component.Load(Path.Combine(path, component))); 
        }
    }
}