using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AndromedaApi.Attributes;

namespace AndromedaApi.AddonTypes
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
                    var type = cls.GetCustomAttribute<ResourceType>();
                    var name = cls.GetCustomAttribute<ResourceName>();
                    if (type != null && name != null)
                    {
                        switch (type.Type)
                        {
                            case "Task": Tasks.Add(new TaskClass(name.Name, cls)); break;
                        }
                    }
                }
            }
        }
    }
}
