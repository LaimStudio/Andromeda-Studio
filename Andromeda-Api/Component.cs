﻿using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using YamlDotNet.Serialization;
using AndromedaApi.Components;

namespace AndromedaApi
{
    public class Component
    {
        public string Name;
        public string Type;
        public string Path;

        public JObject JObjectView;

        public static Component Load(string file)
        {
            var raw = File.ReadAllText(file);
            var deserializer = new DeserializerBuilder().Build();
            var manifestYaml = deserializer.Deserialize(new StringReader(raw));

            var serializer = new SerializerBuilder().JsonCompatible().Build();
            var json = serializer.Serialize(manifestYaml);

            var component = (JObject)JsonConvert.DeserializeObject(json);

            var result = component.ToObject<Component>();
            result.JObjectView = component;
            result.Path = file;
            return result;
        }

        public ProjectTemplate AsProjectTemplate() => JObjectView.ToObject<ProjectTemplate>();
        public Task AsTask()
        {
            var result = JObjectView.ToObject<Task>();
            result.Path = Path;
            return result;
        }
    }
}