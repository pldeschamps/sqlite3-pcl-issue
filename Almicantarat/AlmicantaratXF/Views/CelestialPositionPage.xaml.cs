using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using AlmicantaratXF.Resources;
using AlmicantaratXF.Model;
using System.Collections.ObjectModel;

namespace AlmicantaratXF.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CelestialPositionPage : ContentPage
	{
        private ResourceManager rm = Strings.ResourceManager;
        private Position currentPosition;
        public CelestialPositionPage ()
		{
			InitializeComponent ();
		}
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            currentPosition = BindingContext as Model.Position;
#if DEBUG
            //List<Sight> test = AlmicantaratXF.Views.App.PositionsSightsDB.GetSightsAsync(currentPosition).Result;
#endif
            listView.ItemsSource = AlmicantaratXF.Views.App.PositionsSightsDB.GetSightsCollection(currentPosition);
        }
        async void OnSightSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //déselectionner la ligne pour qu'elle ne soit plus en surbrillance
            (sender as ListView).SelectedItem = null;
            if (e.SelectedItem != null)
            {
                /*
                await Navigation.PushAsync(new SightPage
                {
                    BindingContext = e.SelectedItem as Sight
                });
                */
                await Navigation.PushAsync(new SightPage(e.SelectedItem as Sight, currentPosition));
            }
        }
        async void OnNewSightClicked(object sender, EventArgs args)
        {
            Model.Position currentPosition = BindingContext as Model.Position;
            Sight newSight = new Sight(currentPosition.ID);
            await AlmicantaratXF.Views.App.PositionsSightsDB.SaveSightAsync(newSight);
            /*
            await Navigation.PushAsync(new SightPage
            {
                BindingContext = newSight
            });
            */
            await Navigation.PushAsync(new SightPage(newSight, currentPosition));
        }
        async void OnDelete(object sender, EventArgs args)
        {
            bool answer = await DisplayAlert(
                rm.GetString("warning"),
                rm.GetString("messageDeletePosition"),
                rm.GetString("deleteAnyway"),
                rm.GetString("cancel"));
            //System.Diagnostics.Debug.WriteLine("Answer: " + answer);
            if (answer)
            {
                Model.Position currentPosition = BindingContext as Model.Position;
                //TODO delete sights
                AlmicantaratXF.Views.App.PositionsSightsDB.DeleteSights(currentPosition.ID);
                await AlmicantaratXF.Views.App.PositionsSightsDB.DeletePositionAsync(currentPosition);
                await Navigation.PopAsync();
            }
        }
        async void OnDeleteSight(object sender, EventArgs args)
        {
            SwipeItemView item = sender as SwipeItemView;
            Sight sightToDelete = item.BindingContext as Sight;
            ObservableCollection<Sight> sightsCollection = listView.ItemsSource as ObservableCollection<Sight>;
            if (sightToDelete != null)
            {
                bool answer = await DisplayAlert(
                rm.GetString("warning"),
                rm.GetString("messageDeleteSight"),
                rm.GetString("deleteAnyway"),
                rm.GetString("cancel"));
                //System.Diagnostics.Debug.WriteLine("Answer: " + answer);
                if (answer)
                {
                    await AlmicantaratXF.Views.App.PositionsSightsDB.DeleteSightAsync(sightToDelete);
                    sightsCollection.Remove(sightToDelete);
                    currentPosition.CalculateCelestialPosition();
                }
            }
        }
    }
}