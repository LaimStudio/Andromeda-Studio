using Newtonsoft.Json;
using System;
using System.Collections;
using System.IO;
using System.Reflection;

namespace AndromedaStudio.Settings
{
    public class Manager : ViewModel
    {
        public Manager()
        {
            if (!File.Exists(PathToIdeConfig + @"\setting.json"))
            {
                Save();
            }
            else
            {
                Load();
                Save();
            }
        }
        
        public void Save()
        {
            if(!Directory.Exists(PathToIdeConfig))
                Directory.CreateDirectory(PathToIdeConfig);

            var content = JsonConvert.SerializeObject(this);
            File.WriteAllText(PathToIdeConfig + @"\setting.json", content);
        }
        
        public void Load()
        {
            try
            {
                var file = File.ReadAllText(PathToIdeConfig + @"\setting.json");
                var result = JsonConvert.DeserializeObject<ViewModel>(file);

                Type curType = typeof(ViewModel);
                foreach (FieldInfo property in curType.GetFields())
                {
                    var c = property.GetValue(result);
                    if (c is IList list)
                    {
                        if (list.Count == 0)
                        {
                            c = property.GetValue(this);
                        }
                    }
                    if (c == null)
                    {
                        c = property.GetValue(this);
                    }
                    property.SetValue(this, c);
                }
            }
            catch (Exception)
            {
                Save();
            }
        }
    }
}
