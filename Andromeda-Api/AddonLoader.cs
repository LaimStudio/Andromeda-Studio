using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NodeDeserializers;

namespace AndromedaApi
{
    /// <summary>
    /// Загружает и хранит дополнения
    /// </summary>
    public class AddonLoader
    {
        public List<Addon> Addons = new List<Addon>();

        /// <summary>
        /// Загружает указанное дополнение
        /// </summary>
        /// <param name="path"></param>
        public async Task Load(string path)
        {
            await Task.Run(() =>
            {
                var manifestRaw = File.ReadAllText(Path.Combine(path, "manifest.yml"));
                var deserializer = new DeserializerBuilder().Build();
                var manifest = deserializer.Deserialize<Manifest>(manifestRaw);

                Addons.Add(new Addon(manifest, path));
            });
        }

        /// <summary>
        /// Загружает все дополнения из указанной папки
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task LoadFromDirectory(string path)
        {
            await Task.Run(() =>
            {
                var tasks = new List<Task>();
                foreach (var addon in Directory.GetDirectories(path))
                {
                    tasks.Add(Load(addon));
                }

                Task.WaitAll(tasks.ToArray());
            });
        }
    }
}
