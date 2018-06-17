using System;
using Newtonsoft.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AndromedaTest.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var cs2 = new Class2
            {
                Field1 = "Value1",
                Field2 = "Value2"
            };
            var json = JsonConvert.SerializeObject(cs2);
            var cs1 = JsonConvert.DeserializeObject(json);
            ///var gcs2 = cs1.;
            //Console.WriteLine(gcs2.Field2);
        }

        public class Class1
        {
            public string Field1;
        }

        public class Class2 : Class1
        {
            public string Field2;
        }

    }
}
