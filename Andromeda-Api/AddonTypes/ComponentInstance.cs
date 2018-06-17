using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AndromedaApi.Components;
using AndromedaApi.StaticComponents;
using Newtonsoft.Json;

namespace AndromedaApi.AddonTypes
{
    public class ComponentInstance
    {
        private object _object;
        private string _json;

        public ComponentInstance(object ins)
        {
            _object = ins;
        }

        public ComponentInstance(string json)
        {
            _json = json;
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
        /// Возвращает нестатический компонент в чистом виде (как объект)
        /// </summary>
        /// <returns></returns>
        public object AsObject()
        {
            return _object;
        }

        /// <summary>
        /// Возвращает json статического компонента
        /// </summary>
        /// <returns></returns>
        public string AsJSON()
        {
            return _json;
        }

        /// <summary>
        /// Пребразует компонент как шаблон проекта
        /// </summary>
        /// <returns></returns>
        public ProjectTemplate AsProjectTemplate()
        {
            return JsonConvert.DeserializeObject<ProjectTemplate>(_json);
        }
    }
}
