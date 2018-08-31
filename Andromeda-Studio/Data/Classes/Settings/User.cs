using Newtonsoft.Json;
using System;

namespace AndromedaStudio.Settings
{
    public class User
    {
        [JsonIgnore]
        public string FirstName;

        [JsonIgnore]
        public string LastName;

        public string AccessToken;
    }
}