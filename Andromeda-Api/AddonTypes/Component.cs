#pragma warning disable CS0108

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AndromedaApi.Attributes;
using AndromedaApi.Components;

namespace AndromedaApi.AddonTypes
{
    public class Component
    {
        /// <summary>
        /// Имя компонента
        /// </summary>
        public string Name;

        /// <summary>
        /// Тип компонента
        /// </summary>
        public string Type;

        private ConstructorInfo Constructor;

        /// <summary>
        /// Пытается обработать класс как компонент
        /// </summary>
        /// <param name="cls"></param>
        /// <param name="component"></param>
        /// <returns></returns>
        public static bool TryParse(Type cls, out Component component)
        {
            var name = cls.GetCustomAttribute<ComponentName>();
            var type = cls.GetCustomAttribute<ComponentType>();

            if (type != null && name != null)
            {
                component = new Component
                {
                    Name = name.Name,
                    Type = type.Type,
                    Constructor = cls.GetConstructors().First()
                };
                return true;
            }
            else
            {
                component = null;
                return false;
            }
        }

        /// <summary>
        /// Создает экземпляр компонента
        /// </summary>
        /// <returns></returns>
        public ComponentInstance CreateInstance() => new ComponentInstance(Constructor.Invoke(new object[0]));
    }
}
