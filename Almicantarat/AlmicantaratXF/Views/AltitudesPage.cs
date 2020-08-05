using Xamarin.Forms;
using AlmicantaratXF.Resources;

namespace Almicantarat
{
    class AltitudesPage : ContentPage
    {
        public AltitudesPage()
        {
            ListView listView = new ListView();
            listView.ItemTemplate = new DataTemplate(typeof(AltitudeCell));
            StackLayout stackLayout = new StackLayout();
            stackLayout.Children.Add(listView);
            if (Device.RuntimePlatform == Device.iOS)
                this.Padding = new Thickness(10, 20, 10, 5);
            else
                this.Padding = new Thickness(10, 0, 10, 5);
            this.Content = stackLayout;
            this.Content = stackLayout;
        }

    }
}
