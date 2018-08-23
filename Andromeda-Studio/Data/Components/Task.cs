using System;
using System.Collections.Generic;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using STask = System.Threading.Tasks.Task;
using AndromedaStudio.Classes;

namespace AndromedaStudio.Components
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
                Action<string> arg = Print;
                engine.Operations.Invoke(Action, arg);
            });
        }
    }
}
