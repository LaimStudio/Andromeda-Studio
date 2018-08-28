using Newtonsoft.Json;
using System;

namespace AndromedaStudio.Settings
{
    public class ViewModel
    {
        #region Properties

        public string PathToIdeConfig = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\.AndromedaStudio";
        public Window Window = new Window();
        public User User = new User();

        #endregion
    }

    public class User
    {
        [JsonIgnore]
        public string FirstName;

        [JsonIgnore]
        public string LastName;

        public string AccessToken;
    }

    public class Window
    {
        public double Width = 1025;
        public double Height = 560;
        public double Left;
        public double Top;
        public bool IsMaximized = false;
    }
}
