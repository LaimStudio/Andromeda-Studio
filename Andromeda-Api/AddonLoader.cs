using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AndromedaApi.AddonTypes;
using AndromedaApi.Attributes;

namespace AndromedaApi
{
    public class AddonLoader
    {
        public List<Addon> Addons = new List<Addon>();

        public void Load()
        {
            foreach (var item in Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*.dll"))
            {
                var assembly = Assembly.LoadFrom(item);
                if (assembly.GetCustomAttribute(typeof(AndromedaAddon)) != null)
                    Addons.Add(new Addon(assembly));
            }
        }
    }
}
