using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using STask = System.Threading.Tasks.Task;
using AndromedaStudio.Data.Classes;

namespace AndromedaStudio.Data.Components
{
    public class Task : Component
    {
        public PythonFunction Action;

        public string Output;

        public delegate void OutputHandler(string message);

        public event OutputHandler OnOutput;

        public Task(List<KeyValuePair<object, object>> component)
        {
            Name = (string)component.Find(x => x.Key.ToString() == "name").Value;
            Action = (PythonFunction)component.Find(x => x.Key.ToString() == "action").Value;
        }

        private void Print(string message)
        {
            Output += message + "\n";
            OnOutput?.Invoke(message);
        }

        public async STask Run()
        {
            await STask.Run(() =>
            {
                ScriptEngine engine = Python.CreateEngine();
                Action<string> objectHandle = Print;
                engine.Operations.Invoke(Action, objectHandle);
            });
        }
    }
}
