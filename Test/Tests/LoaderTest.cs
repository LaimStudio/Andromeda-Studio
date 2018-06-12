using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using AndromedaApi;
using AndromedaApi.AddonTypes;
using AndromedaApi.Components;
using System.IO;
using System.Collections.Generic;

namespace AndromedaApiTest.Tests
{
    [TestClass]
    public class LoaderTest
    {
        private List<Addon> Addons = new List<Addon>();
        private AndromedaApi.Components.Task TestTask;

        [TestMethod]
        public async System.Threading.Tasks.Task AsyncLoadTest()
        {
            var loader = new AddonLoader();
            await loader.LoadFromDirectoryAsync(Directory.GetCurrentDirectory());
            Addons.AddRange(loader.Addons);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetTask()
        {
            await AsyncLoadTest();
            var addon = Addons.Find(x => x.Name == "TestAddon");
            var task = addon.Components.Find(x => x.Type == "Task");
            TestTask = task.CreateInstance().AsTask();
        }

        [TestMethod]
        public async System.Threading.Tasks.Task RunTask()
        {
            await GetTask();
            TestTask.OnStatus += (status) => Console.WriteLine("Status: {0}", status);
            TestTask.Run();
        }
    }
}
