using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows;
namespace LightDarkTheme
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private const string RegistryKeyPath = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

        private const string RegistryValueName = "AppsUseLightTheme";

        public enum Theme
        {
            Light,
            Dark
        }

        private ResourceDictionary ThemeDictionary
        {
            get { return Resources.MergedDictionaries[0]; }
            set { Resources.MergedDictionaries[0] = value; }
        }

        private void ChangeTheme(Uri uri)
        {
            ThemeDictionary = new ResourceDictionary() { Source = uri };
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow ParentWindow = new MainWindow();
            ParentWindow.CurrentApplication = this;
            this.MainWindow = ParentWindow;
            ParentWindow.Show();
            ParentWindow.btnTheme.Content = GetWindowsTheme();

        }

        public string GetWindowsTheme()
        {
            var currentUser = WindowsIdentity.GetCurrent();
            string query = string.Format(
                CultureInfo.InvariantCulture,
                @"SELECT * FROM RegistryValueChangeEvent WHERE Hive = 'HKEY_USERS' AND KeyPath = '{0}\\{1}' AND ValueName = '{2}'",
                currentUser.User.Value,
                RegistryKeyPath.Replace(@"\", @"\\"),
                RegistryValueName);

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath))
            {
                object registryValueObject = key?.GetValue(RegistryValueName);
                if (registryValueObject == null)
                {
                    MessageBox.Show("LightTheme");
                    return "LightTheme";
                }

                int registryValue = (int)registryValueObject;

                if(registryValue > 0)
                    MessageBox.Show("DarkTheme");
                else
                    MessageBox.Show("LightTheme");

                return registryValue > 0 ? "DarkTheme" : "LightTheme";
            }

        }

        public void SetTheme()
        {
            //string ThemeName = "";

            //switch (ThemeOption)
            //{
            //    case Theme.Dark: ThemeName = "DarkTheme"; break;
            //    case Theme.Light: ThemeName = "LightTheme"; break;
            //}

            try
            {
                if (!string.IsNullOrEmpty(GetWindowsTheme()))
                    ChangeTheme(new Uri($"Themes/{GetWindowsTheme()}.xaml", UriKind.Relative));
            }
            catch (Exception eee)
            {
            }
        }
    }
}
