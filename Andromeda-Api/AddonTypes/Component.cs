#pragma warning disable CS0108

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
        private string JSON;

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
        /// Пытается обработать статический ресурс как компонент
        /// </summary>
        /// <param name="file"></param>
        /// <param name="component"></param>
        /// <returns></returns>
        public static bool TryParse(string file, out Component component)
        {
            var json = File.ReadAllText(file);
            var stc = JsonConvert.DeserializeObject<StaticComponent>(json);
            if (stc.Name != null && stc.Type != null)
            {
                component = new Component
                {
                    Name = stc.Name,
                    Type = stc.Type,
                    JSON = json
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
        public ComponentInstance CreateInstance()
        {
            if (StaticTypes.Contains(Type))
            {
                return new ComponentInstance(JSON);
            }
            else if(AssemblyTypes.Contains(Type))
            {
                return new ComponentInstance(Constructor.Invoke(new object[0]));
            }
            else
            {
                Console.WriteLine("Syka blyat");
                return null;
            }
        }

        public static string[] AssemblyTypes
        {
            get { return new string[] { "Task" }; }
        }

        public static string[] StaticTypes
        {
            get { return new string[] { "ProjectTemplate" }; }
        }
    }
}
