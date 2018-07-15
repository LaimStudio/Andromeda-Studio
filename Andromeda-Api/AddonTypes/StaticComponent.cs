using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AndromedaApi.AddonTypes
{
    public class StaticComponent
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("type")]
        public string Type;
    }
}
