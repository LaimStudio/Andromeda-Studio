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

        public Task AsTask()
        {
            return (Task)_object;
        }

        public object AsObject()
        {
            return _object;
        }
    }
}
