using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using AndromedaApi;
using AndromedaApi.AddonTypes;
using AndromedaApi.Components;
using AndromedaApi.StaticComponents;
using System.IO;
using System.Collections.Generic;

namespace AndromedaApiTest.Tests
{
    [TestClass]
    public class LoaderTest
    {
        private List<Addon> Addons = new List<Addon>();
        private AndromedaApi.Components.Task TestTask;
        private Component Component;
        private ProjectTemplate Template;

        [TestMethod]
        public async System.Threading.Tasks.Task AsyncLoadTest()
        {
            var loader = new AddonLoader();
            loader.OnException += exception => throw exception;
            await loader.LoadFromManifestAsync(Path.Combine(Directory.GetCurrentDirectory(), "TestAddon", "manifest.json"));
            Addons.AddRange(loader.Addons);
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetStaticComponent()
        {
            await AsyncLoadTest();
            var addon = Addons.Find(x => x.Name == "TestAddon");
            Component = addon.Components.Find(x => x.Type == "ProjectTemplate");
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetProjectTemplate()
        {
            await GetStaticComponent();
            Template = Component.CreateInstance().AsProjectTemplate();
        }

        [TestMethod]
        public async System.Threading.Tasks.Task GetProjectTemplateFiles()
        {
            await GetProjectTemplate();
            foreach(var file in Template.Files)
            {
                Console.WriteLine("{0} with {1}", file.Path, file.Content);
            }
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
