using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndromedaApi.Components;

namespace AndromedaApi.Examples.Task
{
    [TaskInfo(true, true)]
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
