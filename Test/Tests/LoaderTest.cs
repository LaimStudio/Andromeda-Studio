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
        public Package Package;

        [TestMethod]
        public async Task LoadPackage()
        {
            var loader = new PackageLoader();
            await loader.LoadFromDirectory(Directory.GetCurrentDirectory());
            Packages.AddRange(loader.Packages);
        }

        [TestMethod]
        public async Task InitPackage()
        {
            await LoadPackage();

            Package = Packages.Find(x => x.Name == "TestAddon");
            Package.Init();
        }

        [TestMethod]
        public async Task RunTask()
        {
            await InitPackage();
            var task = Package.Components.Find(x => x.Type == "Task").AsTask();
            task.OnOutput += (string message) => Console.WriteLine(message);

            await task.Run();
        }
    }
}
