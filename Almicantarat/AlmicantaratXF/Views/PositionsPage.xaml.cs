using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using AlmicantaratXF.Model;
using AlmicantaratXF.ViewModels;

namespace AlmicantaratXF.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PositionsPage : ContentPage
	{
		public PositionsPage ()
		{
			InitializeComponent ();
		}
        /*protected override async void OnAppearing()
        {
            base.OnAppearing();
            //listView.ItemsSource = await AlmicantaratXF.App.PositionsSightsDB.GetPositionsAsync();
            listView.ItemsSource = App.PositionsSightsDB.GetPositionsSort();
        }*/
        async void OnNewCelestialPositionClicked(object sender, EventArgs args)
        {
            Position newPosition = await (BindingContext as PositionsViewModel).GetNewPositionAsync();
            await Navigation.PushAsync(new CelestialPositionPage
            {
                BindingContext = newPosition
            });

        }
        async void OnCelestialPositionSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //((App)App.Current).ResumeAtTodoId = (e.SelectedItem as TodoItem).ID;
            //Debug.WriteLine("setting ResumeAtTodoId = " + (e.SelectedItem as TodoItem).ID);

            //déselectionner la ligne pour qu'elle ne soit plus en surbrillance
            (sender as ListView).SelectedItem = null;
            if (e.SelectedItem != null)
            {
                await Navigation.PushAsync(new CelestialPositionPage
                {
                    BindingContext = e.SelectedItem as Position
                });
            }
        }
    }
}