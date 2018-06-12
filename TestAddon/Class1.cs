using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndromedaApi.Components;
using AndromedaApi.Attributes;

namespace TestAddon
{
    [ComponentName("HelloWorld")]
    [ComponentType("Task")]
    public class HelloWorldTask : AndromedaApi.Components.Task
    {
        public override void Run()
        {
            Status = TaskStatus.Running;
            Console.WriteLine("Hello world");
            Status = TaskStatus.Finished;
        }
    }
}
