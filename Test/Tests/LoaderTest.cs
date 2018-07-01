using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using AndromedaApi;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AndromedaApiTest.Tests
{
    [TestClass]
    public class LoaderTest
    {
        public List<Package> Packages = new List<Package>();
        public List<Component> Components = new List<Component>();

        [TestMethod]
        public async Task LoadPackage()
        {
            var loader = new PackageLoader();
            await loader.LoadFromDirectory(Directory.GetCurrentDirectory());
            Packages.AddRange(loader.Addons);
        }

        [TestMethod]
        public async Task GetComponent()
        {
            await LoadPackage();
            Components.AddRange(Packages.SelectMany(x => x.Components));
        }

        [TestMethod]
        public async Task ConvertComponent()
        {
            await GetComponent();
            Components.Find(x => x.Type == "template").AsProjectTemplate();
        }
    }
}
