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
        private List<Addon> Addons = new List<Addon>();

        [TestMethod]
        public async Task LoadAddon()
        {
            var loader = new AddonLoader();
            await loader.LoadFromDirectory(Directory.GetCurrentDirectory());
            Addons.AddRange(loader.Addons);
        }
    }
}
