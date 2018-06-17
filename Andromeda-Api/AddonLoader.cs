using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AndromedaApi.AddonTypes;
using AndromedaApi.Attributes;
using Newtonsoft.Json;

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

        public delegate void ExceptionHandler(Exception exception);

        /// <summary>
        /// Возникает при появлении исключения во время загрузки дополнений
        /// </summary>
        public event ExceptionHandler OnException;

        ///// <summary>
        ///// Асинхронно загружает все дополнения, найденные в указанной папке
        ///// </summary>
        ///// <param name="path">Абсолютный путь до папки с дополнениями</param>
        ///// <returns></returns>
        //public async Task LoadFromDirectoryAsync(string path)
        //{
        //    await Task.Run(() =>
        //    {
        //        var tasks = new List<Task>();
        //        foreach (var item in Directory.EnumerateFiles(path, "*.dll"))
        //        {
        //            tasks.Add(LoadFromFileAsync(item));
        //        }
        //        Task.WaitAll(tasks.ToArray());
        //    });
        //}

        /// <summary>
        /// Асинхронно загружает дополнение следуя указанному манифесту дополнения
        /// </summary>
        /// <param name="path">Абсолютный путь до файла дополнения</param>
        public async Task LoadFromManifestAsync(string path)
        {
            try
            {
                await Task.Run(() =>
                {
                    var manifest = JsonConvert.DeserializeObject<AddonManifest>(File.ReadAllText(path));
                    manifest.Path = path;
                    Addons.Add(new Addon(manifest));
                });
            }
            catch (Exception e)
            {
                OnException?.Invoke(e);
            }
        }
    }
}
