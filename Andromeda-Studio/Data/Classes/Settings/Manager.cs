﻿using Newtonsoft.Json;
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
            if (!File.Exists(PathToIde + @"\setting.json"))
            {
                Save();
            }
            else
            {
                Load();
            }
        }
        
        public void Save()
        {
            if(!Directory.Exists(PathToIde))
                Directory.CreateDirectory(PathToIde);

            var content = JsonConvert.SerializeObject(this);
            File.WriteAllText(PathToIde + @"\setting.json", content);
        }
        
        public void Load()
        {
            try
            {
                var file = File.ReadAllText(PathToIde + @"\setting.json");
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
            catch (Exception e)
            {
                Save();
            }
        }
    }
}
