using IronPython.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndromedaStudio.Classes.PackageApi
{
    public class Main
    {
        public Package Package;
        public List<Component> Components;

        public Notifications Notifications = new Notifications();

        public Main(Package package, ref List<Component> components)
        {
            Package = package;
            Components = components;
        }

        public void Load(string path) => Package.Execute(Path.Combine(Package.Path, path));

        public void Register(PythonDictionary component) => Components.Add(Component.Parse(component.ToList()));
    }
}
