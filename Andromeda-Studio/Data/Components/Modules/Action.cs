using AndromedaStudio.Classes;
using IronPython.Hosting;
using IronPython.Runtime;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using STask = System.Threading.Tasks.Task;

namespace AndromedaStudio.Components.Modules
{
    public class Action
    {
        public Action(PythonFunction pythonFunction) {}

        private PythonFunction _function;

        public string Output;

        public delegate void OutputHandler(string message);

        public event OutputHandler OnOutput;

        public async STask Run()
        {
            await STask.Run(() =>
            {
                ScriptEngine engine = Python.CreateEngine();
                Action<string> arg = (string message) =>
                {
                    Output += message + "\n";
                    OnOutput?.Invoke(message);
                };
                engine.Operations.Invoke(_function, arg);
            });
        }
    }
}
