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
            await Task.Run(async () =>
            {
                foreach (var item in Directory.EnumerateFiles(path, "*.dll"))
                {
                    await LoadFromFileAsync(item);
                }
            });
        }

        /// <summary>
        /// Асинхронно загружает дополнение из указаного файла
        /// </summary>
        /// <param name="path">Абсолютный путь до файла дополнения</param>
        public async Task LoadFromFileAsync(string path)
        {
            await Task.Run(() =>
            {
                var assembly = Assembly.LoadFrom(path);
                if (assembly.GetCustomAttribute(typeof(AndromedaAddon)) != null)
                    Addons.Add(new Addon(assembly));
            });
        }

        /// <summary>
        /// Параллельно загружает все дополнения, найденные в указанной папке (экспериментально)
        /// </summary>
        /// <param name="path">Абсолютный путь до папки с дополнениями</param>
        /// <returns></returns>
        public async Task LoadFromDirectoryParallel(string path)
        {
            await Task.Run(() =>
            {
                foreach (var item in Directory.EnumerateFiles(path, "*.dll"))
                {
                    LoadFromFileParallel(item);
                }
            });
        }

        /// <summary>
        /// Асинхронно загружает дополнение из указаного файла и не требует ожидания
        /// </summary>
        /// <param name="path">Абсолютный путь до файла дополнения</param>
        public async void LoadFromFileParallel(string path)
        {
            await Task.Run(() =>
            {
                var assembly = Assembly.LoadFrom(path);
                if (assembly.GetCustomAttribute(typeof(AndromedaAddon)) != null)
                    Addons.Add(new Addon(assembly));
            });
        }
    }
}
