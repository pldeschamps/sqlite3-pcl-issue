using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

using AlmicantaratXF.Views;
using AlmicantaratXF.Resources;
#if DEBUG
using System.Reflection;
#endif
namespace AlmicantaratXF
{
    public class App_ : Application
    {
        public App_()
        {
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
            MainPage = new NavigationPage(new HomePageXAML());
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
