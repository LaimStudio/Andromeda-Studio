using System.IO;
using YamlDotNet.Serialization;

namespace AndromedaApi
{
    public class Component
    {
        private string name;

        public string Name { get => name; set => name = value; }

        public static Component Load(string file)
        {
            var componentRaw = File.ReadAllText(file);
            var deserializer = new DeserializerBuilder().Build();
            return deserializer.Deserialize<Component>(componentRaw);
        }
    }
}