using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndromedaApi.Components;

using AndromedaApi.Attributes;

namespace AndromedaApi.Examples.Task
{
    [ResourceName("HelloWorld")]
    [ResourceType("Task")]
    public class HelloWorldTask : Components.Task
    {
        public override void Run()
        {
            Status = TaskStatus.Running;
            Console.WriteLine("Hello world");
            Status = TaskStatus.Finished;
        }
    }
}
