using AndromedaStudio.Data.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace AndromedaStudio
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }
        private static string _theme;

        public static string Theme
        {
            get => _theme;
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == _theme) return;

                _theme = value;

                ResourceDictionary dict = new ResourceDictionary();
                dict.Source = new Uri(String.Format("Data/Themes/ColorPresets/{0}.xaml", value), UriKind.Relative);

                ResourceDictionary oldDict = null;
                try
                {
                    oldDict = (from d in Application.Current.Resources.MergedDictionaries
                               where d.Source != null && d.Source.OriginalString.StartsWith("Data/Themes/ColorPresets/")
                               select d).First();
                }
                catch (Exception) { }

                if (oldDict != null)
                {
                    int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                    Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(dict);
                }
            }
        }
    }
}
