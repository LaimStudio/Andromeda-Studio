using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace AndromedaApi.Components
{
    public class Task : Component
    {
        public string Action;

        public string Output;

        public delegate void OutputHandler(string message);

        public event OutputHandler OnOutput;

        public void AppendOutput(string message)
        {
            Output = message;
            OnOutput?.Invoke(message);
        }

        public async Task<string> Run()
        {
            return await System.Threading.Tasks.Task.Run(() =>
            {
                ScriptEngine engine = Python.CreateEngine();
                dynamic scope = engine.CreateScope();
                scope.SetVariable("AndromedaApi", System.Reflection.Assembly.GetExecutingAssembly());
                scope.task = this;
                engine.ExecuteFile(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Path), Action), scope);
                return Output;
            });
        }
    }
}
