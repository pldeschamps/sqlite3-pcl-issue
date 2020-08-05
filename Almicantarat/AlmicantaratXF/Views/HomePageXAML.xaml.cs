using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using AlmicantaratSharedProject.Ephemeris;

namespace AlmicantaratXF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageXAML : ContentPage
    {
        public HomePageXAML()
        {
            InitializeComponent();

        }
        protected override async void OnAppearing()
        {
            if (App.AppStarts)
            {
                await AskLocationPermission();
                App.AppStarts = false;
            }
        }
        async void OnButtonSettingsClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new SettingsPageXAML());
        }
        async void OnButtonPositionsClicked(object sender, EventArgs args)
        {
//            Device.BeginInvokeOnMainThread(async () =>
//            {
                await Navigation.PushAsync(new PositionsPage());
//            });
        }
        private async Task AskLocationPermission()
        {
            if (Device.RuntimePlatform != Device.UWP) //TODO: Until https://github.com/xamarin/Xamarin.Forms/issues/9756 is solved
            {

                PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        System.Resources.ResourceManager rm = AlmicantaratXF.Resources.Strings.ResourceManager;

                        bool answer = await this.DisplayAlert(
                            rm.GetString("alertLocationPermission"),
                            rm.GetString("questionLocationPermission"),
                            rm.GetString("answerYes"),
                            rm.GetString("answerNo"));
                        if (answer)
                        {
                            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
                            if (status == PermissionStatus.Granted)
                                AlmicantaratXF.Model.LocationReference.Reference = false;
                            else
                                AlmicantaratXF.Model.LocationReference.Reference = true;
                        }
                        else
                            AlmicantaratXF.Model.LocationReference.Reference = false;
                    });
                }
            }
        }
    }
}