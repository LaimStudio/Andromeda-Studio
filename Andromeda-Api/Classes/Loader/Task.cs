#pragma warning disable CS0108

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AndromedaApi.Components;

namespace AndromedaApi.Classes.Loader
{
    public class TaskClass
    {
        public Type Type;
        public string Name;

        public ConstructorInfo Constructor;

        public TaskClass(Type task)
        {
            Type = task;
            Name = task.Name.Replace("Task", "");
            Constructor = task.GetConstructors().First();
        }
    }

    public class TaskInstance : Components.Task
    {
        public object Instance;

        public MethodInfo RunMethod;

        public override void Run() => RunMethod.Invoke(Instance, new object[0]);


        //Обязательно перезаписать
        /// <summary>
        /// Возникает при изменении статуса
        /// </summary>
        public event StatusHandler OnStatus;

        public TaskInstance(TaskClass task)
        {
            Instance = task.Constructor.Invoke(new object[0]);
            task.Type.GetEvent("OnStatus").AddEventHandler(Instance, new StatusHandler(i => OnStatus?.Invoke(i)));
            RunMethod = task.Type.GetMethod("Run");
            OnStatus += OnRemoteStatus;
        }

        private void OnRemoteStatus(TaskStatus status)
        {
            Status = status;
        }
    }
}
