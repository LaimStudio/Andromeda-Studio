using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using AndromedaApi;
using System.IO;
using System.Collections.Generic;
using System.Linq;

using ATask = AndromedaApi.Components.Task;

namespace AndromedaApiTest.Tests
{
    [TestClass]
    public class LoaderTest
    {
        public List<Package> Packages = new List<Package>();
        public List<Component> Components = new List<Component>();
        public ATask Task;

        [TestMethod]
        public async Task LoadPackage()
        {
            var loader = new PackageLoader();
            await loader.LoadFromDirectory(Directory.GetCurrentDirectory());
            Packages.AddRange(loader.Addons);
        }

        [TestMethod]
        public async Task GetComponents()
        {
            await LoadPackage();
            Components.AddRange(Packages.SelectMany(x => x.Components));
        }

        [TestMethod]
        public async Task ConvertComponents()
        {
            await GetComponents();
            Components.Find(x => x.Type == "template").AsProjectTemplate();
            Task = Components.Find(x => x.Type == "task").AsTask();
        }

        [TestMethod]
        public async Task RunTask()
        {
            await ConvertComponents();
            Task.OnOutput += message => Console.WriteLine(message);
            await Task.Run();
        }
    }
}
