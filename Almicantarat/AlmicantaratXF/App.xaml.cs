using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AlmicantaratXF.Views;
using AlmicantaratXF.Resources;
using AlmicantaratXF.Data;
using AlmicantaratSharedProject.Ephemeris;

#if DEBUG
using System.Reflection;
#endif

namespace AlmicantaratXF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        static SettingsDatabase settingsDatabase;
        static PositionsSightsDatabase positionsSightsDatabase;
        public static bool AppStarts;
        public App()
        {
            InitializeComponent();
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                // determine the correct, supported .NET culture
                var ci = DependencyService.Get<ILocalize>().GetCurrentCultureInfo();
                Strings.Culture = ci; // set the RESX for resource localization
                DependencyService.Get<ILocalize>().SetLocale(ci); // set the Thread for locale-aware methods
            }

#if DEBUG
            var assembly = typeof(Strings).GetTypeInfo().Assembly; // "EmbeddedImages" should be a class in your app
            foreach (var res in assembly.GetManifestResourceNames())
            {
                System.Diagnostics.Debug.WriteLine("found resource: " + res);
                System.Diagnostics.Debug.WriteLine("Current Culture: " + System.Globalization.CultureInfo.CurrentCulture);
            }
#endif
            // The root page of your application
            AppStarts = true;
            MainPage = new NavigationPage(new HomePageXAML());
        }

        public static SettingsDatabase SettingsDB
        {
            get
            {
                if (settingsDatabase == null)
                {
                    settingsDatabase = new SettingsDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "SettingsSQLite.db3"));
                }
                return settingsDatabase;
            }
        }
        public static PositionsSightsDatabase PositionsSightsDB
        {
            get
            {
                if (positionsSightsDatabase == null)
                {
                    positionsSightsDatabase = new PositionsSightsDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PositionsSightsSQLite.db3"));
                }
#if DEBUG
                System.Diagnostics.Debug.WriteLine("db3 folder: " +  Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
#endif

                return positionsSightsDatabase;
            }
        }

        

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}