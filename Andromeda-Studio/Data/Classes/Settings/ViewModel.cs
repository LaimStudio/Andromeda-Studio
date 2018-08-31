using Newtonsoft.Json;
using System;

namespace AndromedaStudio.Settings
{
    public class ViewModel
    {
        #region Properties

        public string PathToIdeConfig = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\.AndromedaStudio";
        public string Language = null;

        public string Theme = "Day";
        public string AltColor = "Blue";

        public Window Window = new Window();
        public User User = new User();

        #endregion
    }
}
