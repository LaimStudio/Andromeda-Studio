using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AndromedaApi.Attributes;
using AndromedaApi.Components;

namespace AndromedaApi.AddonTypes
{
    public class Addon
    {
        public Assembly Assembly;

        public List<Component> Components = new List<Component>();

        public string Name;

        public Addon(Assembly assembly)
        {
            Assembly = assembly;
            Name = Assembly.GetName().Name;
            foreach (var module in Assembly.GetModules())
            {
                foreach (var cls in module.GetTypes())
                {
                    Component component;
                    if (Component.TryParse(cls, out component))
                        Components.Add(component);
                }
            }
        }
    }
}
