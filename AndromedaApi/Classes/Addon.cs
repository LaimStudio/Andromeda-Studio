using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using AndromedaApi.Classes.Loader;

namespace AndromedaApi.Classes
{
    public class Addon
    {
        public Assembly Assembly;

        public List<TaskClass> Tasks = new List<TaskClass>();

        public string Name;

        public Addon(Assembly assembly)
        {
            Assembly = assembly;
            Name = Assembly.GetName().Name;
            foreach (var module in Assembly.GetModules())
            {
                foreach (var cls in module.GetTypes())
                {
                    if (cls.Name.EndsWith("Task"))
                        Tasks.Add(new TaskClass(cls));
                }
            }
        }
    }
}
