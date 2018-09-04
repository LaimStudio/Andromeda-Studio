using Microsoft.Scripting.Hosting;
using System.Collections.Generic;
using IronPython.Hosting;

using SPath = System.IO.Path;
using AndromedaStudio.Classes.PackageApi;

namespace AndromedaStudio.Classes
{
    public class Package
    {
        public string Name;
        public string Version;
        public string Author;
        public string Main;
        public string Path;

        private bool _isTest = false;

        public List<Component> Components = new List<Component>();

        private ScriptScope Scope;
        private ScriptEngine Engine;

        public static Package CreateTestPackage()
        {
            var package = new Package
            {
                Name = "TestPackage",
                Author = "Andromeda Studio",
                _isTest = true
            };
            package.Init();
            return package;
        }

        /// <summary>
        /// Инициализирует дополнение
        /// </summary>
        public void Init()
        {
            var api = new Main(this, ref Components);

            Engine = Python.CreateEngine();
            Scope = Engine.CreateScope();
            Scope.SetVariable("AndromedaApi", api);

            if(!_isTest)
                Execute(SPath.Combine(Path, Main));
        }

        public void ExecuteCode(string code) => Engine.Execute(code, Scope);

        public void Execute(string path) => Engine.ExecuteFile(path, Scope);
    }
}