using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using AndromedaApi;
using System.IO;
using System.Collections.Generic;

namespace AndromedaApiTest.Tests
{
    [TestClass]
    public class LoaderTest
    {
        public List<Package> Addons = new List<Package>();

        [TestMethod]
        public async Task LoadAddon()
        {
            var loader = new PackageLoader();
            await loader.LoadFromDirectory(Directory.GetCurrentDirectory());
            Addons.AddRange(loader.Addons);
        }
    }
}
