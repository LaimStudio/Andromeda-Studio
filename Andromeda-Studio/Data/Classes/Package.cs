using Microsoft.Scripting.Hosting;
using System.Collections.Generic;
using System.IO;
using IronPython.Hosting;

using SPath = System.IO.Path;

namespace AndromedaStudio.Data.Classes
{
    public class Package
    {
        public string Name;
        public string Version;
        public string Author;
        public string Main;
        public string Path;

        public List<Component> Components = new List<Component>();

        private ScriptScope Scope;
        private ScriptEngine Engine;

        /// <summary>
        /// Инициализирует дополнение
        /// </summary>
        public void Init()
        {
            var api = new PackageApi(this, ref Components);

            Engine = Python.CreateEngine();
            Scope = Engine.CreateScope();
            Scope.SetVariable("AndromedaApi", api);

            Execute(SPath.Combine(Path, Main));
        }

        public void Execute(string path) => Engine.ExecuteFile(path, Scope);
    }
}