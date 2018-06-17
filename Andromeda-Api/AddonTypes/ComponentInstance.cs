using System;
using System.Collections.Generic;
using System.IO;
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

        public string AddonDirectory;

        public ComponentInstance(object ins, string addpath)
        {
            _object = ins;
            AddonDirectory = addpath;
        }

        public ComponentInstance(string json, string addpath)
        {
            _json = json;
            AddonDirectory = addpath;
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
            var template = JsonConvert.DeserializeObject<ProjectTemplate>(_json);
            var files = new List<ProjectFile>();
            foreach(var item in template.Files)
            {
                var file = item;
                if (item.File!= null)
                {
                    file.Content = File.ReadAllText(Path.Combine(AddonDirectory, file.File));
                }
                files.Add(file);
            }
            template.Files = files.ToArray();
            return template;
        }
    }
}
