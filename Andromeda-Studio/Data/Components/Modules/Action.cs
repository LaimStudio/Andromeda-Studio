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
        public Action(PythonFunction action) => _function = action;

        private PythonFunction _function;

        public async STask Run()
        {
            await STask.Run(() =>
            {
                ScriptEngine engine = Python.CreateEngine();
                engine.Operations.Invoke(_function);
            });
        }
    }
}
