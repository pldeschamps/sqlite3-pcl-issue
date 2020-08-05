using Xamarin.Forms;
using AlmicantaratXF.Resources;

namespace Almicantarat
{
    public class AltitudeCell : ViewCell
    {
        public AltitudeCell()
        {
            //instantiate each of our views
            StackLayout cellWrapper = new StackLayout();
            StackLayout horizontalLayout = new StackLayout();
            Label star = new Label();
            Label time = new Label();
            Label altitude = new Label();
            Label intercept = new Label();

            //set bindings
            star.SetBinding(Label.TextProperty, "star");
            time.SetBinding(Label.TextProperty, "time");
            altitude.SetBinding(Image.SourceProperty, "altitude");
            intercept.SetBinding(Image.SourceProperty, "intercept");

            //Set properties for desired design
            cellWrapper.BackgroundColor = Color.Navy;
            horizontalLayout.Orientation = StackOrientation.Horizontal;
            intercept.HorizontalOptions = LayoutOptions.EndAndExpand;
            star.TextColor = Color.Yellow;
            time.TextColor = Color.Olive;
            altitude.TextColor = Color.Purple;
            intercept.TextColor = Color.Lime;

            //add views to the view hierarchy
            horizontalLayout.Children.Add(star);
            horizontalLayout.Children.Add(time);
            horizontalLayout.Children.Add(altitude);
            horizontalLayout.Children.Add(intercept);
            cellWrapper.Children.Add(horizontalLayout);
            View = cellWrapper;
            
            //TODO binding
            //https://developer.xamarin.com/guides/xamarin-forms/user-interface/listview/customizing-cell-appearance/
        }
    }
}
