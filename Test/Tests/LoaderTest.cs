using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AndromedaApi;
using AndromedaApi.AddonTypes;
using AndromedaApi.Components;

namespace AndromedaApiTest.Tests
{
    [TestClass]
    public class LoaderTest
    {
        [TestMethod]
        public void LoadTest()
        {
            var loader = new AddonLoader();
            loader.Load();
            var addon = loader.Addons.Find(x => x.Name == "TestAddon");
            var task = addon.Tasks.Find(x => x.Name == "HelloWorld");
            var inst = new TaskInstance(task);
            inst.OnStatus += (Task.TaskStatus status) => Console.WriteLine("Status changed to {0}", status);
            inst.Run();
        }
    }
}
