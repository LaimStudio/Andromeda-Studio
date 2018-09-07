using AndromedaStudio.Components;
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
        private Package _package;
        private List<Component> _components;

        public Notifications Notifications = new Notifications();
        public Editor Editor = new Editor();

        public Main(Package package, ref List<Component> components)
        {
            _package = package;
            _components = components;
        }

        public void Load(string path) => _package.Execute(Path.Combine(_package.Path, path));

        public void Register(PythonDictionary component) => _components.Add(Component.Parse(component.ToList()));

        public void DefineMenu(PythonDictionary values) => Component.Parse(values.ToList()).Cast<MenuItem>().Bind();
    }
}
