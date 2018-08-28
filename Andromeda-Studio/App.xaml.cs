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
        public static List<CultureInfo> Languages { get; } = new List<CultureInfo>();

        public App()
        {
            InitializeComponent();
            LanguageChanged += App_LanguageChanged;

            Languages.Clear();
            Languages.Add(new CultureInfo("en"));
            Languages.Add(new CultureInfo("ru"));
            Languages.Add(new CultureInfo("zh"));
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
                if (value == null) value = CultureInfo.CurrentCulture;
                if (value == System.Threading.Thread.CurrentThread.CurrentUICulture) return;

                if (!App.Languages.Contains(value))
                {
                    value = new CultureInfo(value.TwoLetterISOLanguageName);
                }

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

                try
                {
                    dict.Source = new Uri(String.Format("Data/Themes/ColorPresets/{0}.xaml", value), UriKind.Relative);
                }
                catch (Exception)
                {
                    value = _theme = "Day";
                    dict.Source = new Uri(String.Format("Data/Themes/ColorPresets/{0}.xaml", value), UriKind.Relative);
                }

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

        #region AltColor
        private static string _altColor;

        public static string AltColor
        {
            get => _altColor;
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == _altColor) return;

                _altColor = value;

                ResourceDictionary dict = new ResourceDictionary();

                try
                {
                    dict.Source = new Uri(String.Format("Data/Themes/ColorPallete/{0}.xaml", value), UriKind.Relative);
                }
                catch (Exception) {
                    value = _altColor = "Blue";
                    dict.Source = new Uri(String.Format("Data/Themes/ColorPallete/{0}.xaml", value), UriKind.Relative);
                }

                ResourceDictionary oldDict = null;
                try
                {
                    oldDict = (from d in Current.Resources.MergedDictionaries
                               where d.Source != null && d.Source.OriginalString.StartsWith("Data/Themes/ColorPallete/")
                               && d.Source.OriginalString != "Data/Themes/ColorPallete/Gray.xaml"
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
