using AndromedaStudio.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AndromedaStudio.Classes
{
    public class Component
    {
        public string Name;
        public string Type;
        public string PackageName;

        private List<KeyValuePair<object, object>> _values;

        public static Component Parse(List<KeyValuePair<object, object>> values)
        {
            var result = new Component();
            result._values = values;
            //result.CommonCast();
            //result.AutoCast();
            return result;
        }

        public void CommonCast()
        {
            foreach (var property in typeof(Component).GetFields())
                property.SetValue(this, _values.FirstOrDefault(x => x.Key.ToString().ToLower() == property.Name.ToLower()).Value);
        }

        public T Cast<T>()
        {
            var type = typeof(T);
            var result = type.GetConstructors().First().Invoke(null);
            foreach (var property in type.GetFields())
                property.SetValue(result, _values.FirstOrDefault(x => x.Key.ToString().ToLower() == property.Name.ToLower()).Value);
            return (T)result;
        }

        public void AutoCast()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var type = assembly.GetTypes().FirstOrDefault(x => x.BaseType == typeof(Component) && x.Name.ToLower() == Type.ToLower());
            var instance = type.GetConstructors().First().Invoke(null);
            foreach (var property in type.GetFields())
                property.SetValue(instance, _values.FirstOrDefault(x => x.Key.ToString().ToLower() == property.Name.ToLower()).Value);
            type.GetMethod("Init").Invoke(instance, null);
        }
    }
}