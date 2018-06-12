using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndromedaApi.Components;

namespace AndromedaApi.AddonTypes
{
    public class ComponentInstance
    {
        private object _object;

        public ComponentInstance(object ins)
        {
            _object = ins;
        }

        /// <summary>
        /// Преобразует компонент как задачу
        /// </summary>
        /// <returns></returns>
        public Task AsTask()
        {
            return (Task)_object;
        }

        /// <summary>
        /// Возвращает компонент в чистом виде (как объект)
        /// </summary>
        /// <returns></returns>
        public object AsObject()
        {
            return _object;
        }
    }
}
