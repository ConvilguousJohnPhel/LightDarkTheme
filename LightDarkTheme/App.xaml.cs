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
            SetTheme();
            MainWindow ParentWindow = new MainWindow();
            ParentWindow.CurrentApplication = this;
            this.MainWindow = ParentWindow;
            ParentWindow.Show();
            ParentWindow.btnTheme.Content = GetWindowsTheme();

        }

        public string GetWindowsTheme()
        {
            var currentUser = WindowsIdentity.GetCurrent();

            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(RegistryKeyPath))
            {
                object registryValueObject = key?.GetValue(RegistryValueName);
                if (registryValueObject == null)
                {
                    return "LightTheme";
                }

                int registryValue = (int)registryValueObject;

                return registryValue > 0 ? "LightTheme" : "DarkTheme";
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
