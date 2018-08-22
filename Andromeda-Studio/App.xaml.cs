using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace AndromedaStudio
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            WalkDictionary(this.Resources);

            base.OnStartup(e);
        }

        private static void WalkDictionary(ResourceDictionary resources)
        {
            foreach (System.Collections.DictionaryEntry entry in resources)
            {
            }

            foreach (ResourceDictionary rd in resources.MergedDictionaries)
                WalkDictionary(rd);
        }

        #region Languages
        private static List<CultureInfo> m_Languages = new List<CultureInfo>();

        public static List<CultureInfo> Languages
        {
            get
            {
                return m_Languages;
            }
        }

        public App()
        {
            InitializeComponent();
            App.LanguageChanged += App_LanguageChanged;

            m_Languages.Clear();
            m_Languages.Add(new CultureInfo("en-US"));
            m_Languages.Add(new CultureInfo("ru-RU"));
            m_Languages.Add(new CultureInfo("zh-ZH"));
        }

        public static event EventHandler LanguageChanged;

        public static CultureInfo Language
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == System.Threading.Thread.CurrentThread.CurrentUICulture) return;

                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

                ResourceDictionary dict = new ResourceDictionary();
                dict.Source = new Uri(String.Format("Data/Languages/{0}.xaml", value.Name), UriKind.Relative);

                ResourceDictionary oldDict = null;
                try
                {
                    oldDict = (from d in Current.Resources.MergedDictionaries
                               where d.Source != null && d.Source.OriginalString.StartsWith("Data/Languages/")
                               select d).First();
                }
                catch (Exception) { }

                if (oldDict != null)
                {
                    int ind = Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Current.Resources.MergedDictionaries.Remove(oldDict);
                    Current.Resources.MergedDictionaries.Insert(ind, dict);
                }
                else
                {
                    Current.Resources.MergedDictionaries.Add(dict);
                }

                LanguageChanged(Current, new EventArgs());
            }
        }

        private void App_LanguageChanged(Object sender, EventArgs e)
        {
            //save
        }
        #endregion

        #region Theme
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
                    oldDict = (from d in Current.Resources.MergedDictionaries
                               where d.Source != null && d.Source.OriginalString.StartsWith("Data/Themes/ColorPresets/")
                               select d).First();
                }
                catch (Exception) { }

                if (oldDict != null)
                {
                    int ind = Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Current.Resources.MergedDictionaries.Remove(oldDict);
                    Current.Resources.MergedDictionaries.Insert(ind, dict);
                }
                else
                {
                    Current.Resources.MergedDictionaries.Add(dict);
                }
            }
        }
        #endregion
    }
}
