using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AndromedaApi.Examples.ProjectTemplate;
using System.Linq;

namespace AndromedaTest.Tests
{
    [TestClass]
    public class ProjectTemplate
    {
        [TestMethod]
        public void GetContentByPath()
        {
            var template = new HelloWorldTemplate();
            var structure = template.GetStructure();
            var main = structure.ToList().Find(x => x.Path == "/main.php");
            Assert.AreEqual(main.Content, "echo \"Hello world\"");
        }
    }
}
