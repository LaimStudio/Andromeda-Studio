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

        public async void Run()
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                ScriptEngine engine = Python.CreateEngine();
                engine.ExecuteFile(System.IO.Path.Combine(System.IO.Path.GetFullPath(Path), Action));
            });
        }
    }
}
