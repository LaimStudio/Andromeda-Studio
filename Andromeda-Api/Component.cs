using System.Collections.Generic;
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

        public ProjectTemplate AsProjectTemplate() => (ProjectTemplate)this;
        public Task AsTask() => (Task)this;
    }
}