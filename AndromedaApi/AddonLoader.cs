using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using AndromedaApi.Classes;

namespace AndromedaApi
{
    public class AddonLoader
    {
        public List<Addon> Addons = new List<Addon>();

        public void Load()
        {
            foreach (var item in Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*Addon.dll"))
            {
                var assembly = Assembly.LoadFrom(item);
                Addons.Add(new Addon(assembly));
                Console.WriteLine("{0} loaded", assembly.GetName().Name);
            }
        }
    }
}
