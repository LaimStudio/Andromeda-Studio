using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndromedaApi.Components
{
    public abstract class Target
    {
        public virtual Task[] GetTaskList()
        {
            return new Task[0];
        }
    }
}
