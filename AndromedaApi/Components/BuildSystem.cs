using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndromedaApi.Components
{
    public abstract class BuildSystem : Task
    {
        /// <summary>
        /// Имя системы сборки
        /// </summary>
        public string Name;

        private string Executable;

        public override void Run()
        {
            
            Status = TaskStatus.Running;
        }
    }
}
