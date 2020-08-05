using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AlmicantaratXF.Resources;
using AlmicantaratXF.ViewModels;
using AlmicantaratXF.Model;

namespace AlmicantaratXF.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SightPage : ContentPage
	{
        private ResourceManager rm = Strings.ResourceManager;
        private Position positionOwningTheSight;
        public SightPage (Sight currentSight, Position position)
		{
            InitializeComponent ();
            positionOwningTheSight = position;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }
        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
        }
        async void OnNowClicked(object sender, EventArgs args)
        {
            // C'est commenté car l'event Clicked est de l'UI, pas du Model
        //    labelSightTime.Text = DateTime.Now.ToString();
        //    Position position;
        //    position = (labelLat.BindingContext as GeoLocationUpdateViewModel).Position;
        //    labelSightLat.Text = position.StrLat;
        //    labelSightLon.Text = position.StrLon;
        }
        async void OnSelectOnSkyClicked(object sender, EventArgs args)
        {
        }
        async void OnAltitudeFocused(object sender, EventArgs args)
        {
            if(EntryAltitude.Text=="00°00.0'")
            {
                EntryAltitude.Text = "";
            }
        }
        async void OnDelete(object sender, EventArgs args)
        {
            bool answer = await DisplayAlert(
                rm.GetString("warning"),
                rm.GetString("messageDeleteSight"),
                rm.GetString("deleteAnyway"),
                rm.GetString("cancel"));
            //System.Diagnostics.Debug.WriteLine("Answer: " + answer);
            if (answer)
            {
                positionOwningTheSight.CalculateCelestialPosition();
                await Navigation.PopAsync();
            }
        }
    }
}