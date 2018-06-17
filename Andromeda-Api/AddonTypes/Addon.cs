using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using AndromedaApi.Attributes;
using AndromedaApi.Components;

namespace AndromedaApi.AddonTypes
{
    public class Addon
    {

        public List<Component> Components = new List<Component>();

        public string Name;

        public Addon(AddonManifest manifest)
        {
            Name = manifest.Name;
            foreach (var item in manifest.Assemblies)
            {
                var assembly = Assembly.LoadFrom(Path.Combine(Path.GetDirectoryName(manifest.Path), item));
                foreach (var module in assembly.GetModules())
                {
                    foreach (var cls in module.GetTypes())
                    {
                        Component component;
                        if (Component.TryParse(cls, out component, Path.GetDirectoryName(manifest.Path)))
                            Components.Add(component);
                    }
                }
            }

            foreach(var item in manifest.Resources)
            {
                Component component;
                if (Component.TryParse(Path.Combine(Path.GetDirectoryName(manifest.Path), item), out component, Path.GetDirectoryName(manifest.Path)))
                    Components.Add(component);
            }
        }
    }

    public class AddonManifest
    {
        public string Name;
        public string Path;
        public string[] Assemblies;
        public string[] Resources;
    }
}
