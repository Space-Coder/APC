using APC.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace APC
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static ApplicationDbContext db = new ApplicationDbContext();
        public App()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
            }
            StartupUri = 
                APC.Properties.Settings.Default.IsFirstLaunch == true ? new Uri("MVVM/View/ChangeSecurity.xaml" , UriKind.RelativeOrAbsolute) : new Uri("MVVM/View/MainWindow.xaml", UriKind.RelativeOrAbsolute);
            ChangeTheme(APC.Properties.Settings.Default.ThemeSettings);
        }


        public static void ChangeTheme(bool isDarkTheme)
        {
            ResourceDictionary themeDictionary = new ResourceDictionary();
            ResourceDictionary mahApssTheme = new ResourceDictionary();
            if (isDarkTheme == true)
            {
                themeDictionary.Source = new Uri("pack://application:,,,/Resources/Styles/Colors/Theme.Dark.xaml", UriKind.RelativeOrAbsolute);
                mahApssTheme.Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Themes/Dark.Blue.xaml", UriKind.RelativeOrAbsolute);
            }
            else
            {
                themeDictionary.Source = new Uri("pack://application:,,,/Resources/Styles/Colors/Theme.Light.xaml", UriKind.RelativeOrAbsolute);
                mahApssTheme.Source = new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Themes/Light.Blue.xaml", UriKind.RelativeOrAbsolute);
            }
            App.Current.Resources.MergedDictionaries.Add(themeDictionary);
            App.Current.Resources.MergedDictionaries.Add(mahApssTheme);
            APC.Properties.Settings.Default.ThemeSettings = isDarkTheme;
            APC.Properties.Settings.Default.Save();
        }
    }
}
