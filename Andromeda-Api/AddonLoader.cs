using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AndromedaApi.AddonTypes;
using AndromedaApi.Attributes;

namespace AndromedaApi
{
    /// <summary>
    /// Загружает и хранит дополнения
    /// </summary>
    public class AddonLoader
    {
        /// <summary>
        /// Список загруженных дополнений
        /// </summary>
        public List<Addon> Addons = new List<Addon>();

        /// <summary>
        /// Загружает все дополнения, найденные в указанной папке
        /// </summary>
        /// <param name="path">Абсолютный путь до папки с дополнениями</param>
        public void LoadFromDirectory(string path)
        {
            foreach (var item in Directory.EnumerateFiles(path, "*.dll"))
            {
                LoadFromFile(item);
            }
        }

        public delegate void ExceptionHandler(Exception exception);

        /// <summary>
        /// Возникает при появлении исключения во время загрузки дополнений
        /// </summary>
        public event ExceptionHandler OnException;

        /// <summary>
        /// Загружает дополнение из указаного файла
        /// </summary>
        /// <param name="path">Абсолютный путь до файла дополнения</param>
        public void LoadFromFile(string path)
        {
            var assembly = Assembly.LoadFrom(path);
            if (assembly.GetCustomAttribute(typeof(AndromedaAddon)) != null)
                Addons.Add(new Addon(assembly));
        }

        /// <summary>
        /// Асинхронно загружает все дополнения, найденные в указанной папке
        /// </summary>
        /// <param name="path">Абсолютный путь до папки с дополнениями</param>
        /// <returns></returns>
        public async Task LoadFromDirectoryAsync(string path)
        {
            await Task.Run(() =>
            {
                foreach (var item in Directory.EnumerateFiles(path, "*.dll"))
                {
                    LoadFromFileAsync(item);
                }
            });
        }

        /// <summary>
        /// Асинхронно загружает дополнение из указаного файла
        /// </summary>
        /// <param name="path">Абсолютный путь до файла дополнения</param>
        public async void LoadFromFileAsync(string path)
        {
            try
            {
                await Task.Run(() =>
                {
                    var assembly = Assembly.LoadFrom(path);
                    if (assembly.GetCustomAttribute(typeof(AndromedaAddon)) != null)
                        Addons.Add(new Addon(assembly));
                });
            }
            catch (Exception e)
            {
                OnException?.Invoke(e);
            }
        }
    }
}
