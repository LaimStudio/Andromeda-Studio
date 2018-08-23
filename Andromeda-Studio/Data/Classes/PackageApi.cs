using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Runtime;

using SPath = System.IO.Path;
using ATask = AndromedaStudio.Components.Task;

namespace AndromedaStudio.Classes
{
    public class PackageApi
    {
        public Package Package;
        public List<Component> Components;

        //Debug api
        public PackageApi()
        {

        }

        public PackageApi(Package package, ref List<Component> components)
        {
            Package = package;
            Components = components;
        }

        public void Load(string path) => Package.Execute(SPath.Combine(Package.Path, path));

        public class Notification
        {
            public static void Show(string title, string message = "")
            {
                Database.MainWindow.Dispatcher.Invoke(new Action(() =>
                {
                    Database.NotificationsManager.Add(new Notifications.Notification
                    {
                        Content = title,
                        Description = message
                    });
                }));
            }
        }

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
