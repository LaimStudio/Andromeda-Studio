using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AndromedaApi.Examples.Task;
using System.Threading;
using static AndromedaApi.Components.Task;

namespace AndromedaApiTest.Tests
{
    [TestClass]
    public class TaskTest
    {
        [TestMethod]
        public void HelloWorldTaskTest()
        {
            var task = new HelloWorldTask();
            task.Run();
        }

        [TestMethod]
        public void AsyncTaskTest()
        {
            var task = new AsyncTask();
            task.Run();
            Thread.Sleep(100);
            task.Status = TaskStatus.Stopped;
            Thread.Sleep(100);
            task.Status = TaskStatus.Running;
            Thread.Sleep(100);
            task.Status = TaskStatus.Cancelled;
        }
    }
}
