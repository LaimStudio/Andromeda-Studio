using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Runtime;

using SPath = System.IO.Path;
using ATask = AndromedaStudio.Data.Components.Task;

namespace AndromedaStudio.Data.Classes
{
    public class PackageApi
    {
        public Package Package;
        public List<Component> Components;

        public PackageApi(Package package, ref List<Component> components)
        {
            Package = package;
            Components = components;
        }

        public void Load(string path) => Package.Execute(SPath.Combine(Package.Path, path));

        public void Register(PythonDictionary component)
        {
            var componentL = component.ToList();
            switch (componentL.Find(x => x.Key.ToString() == "type").Value)
            {
                case "Task":
                {
                    var result = new ATask(componentL);
                    result.Type = "Task";
                    Components.Add(result);
                    break;
                }
            }
        }
    }
}
