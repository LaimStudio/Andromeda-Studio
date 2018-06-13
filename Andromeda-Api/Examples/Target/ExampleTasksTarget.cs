using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndromedaApi.Components;

namespace AndromedaApi.Examples.Target
{
    public class ExampleTasksTarget : Components.Target
    {
        public override Components.Task[] GetTaskList()
        {
            return new Components.Task[]
            {
                new Task.HelloWorldTask(),
                new Task.AsyncTask()
            };
        }
    }
}
