using Xamarin.Forms;

namespace Almicantarat
{
    class HomePage : ContentPage
    {
        public HomePage()
        {
            Button buttonTheory = new Button
            {
                Text = Strings.buttonTheory
            };
            buttonTheory.Clicked += async (sender, args) =>
            {
                await Navigation.PushAsync(new LessonPage());
            };
            Button buttonPratice = new Button
            {
                Text = "Pratice"
            };
            Button buttonPreferences = new Button
            {
                Text = "Preferences"
            };
            StackLayout stackLayout = new StackLayout
            {
                Children =
            {
                buttonTheory,
                buttonPratice,
                buttonPreferences
            }
            };
            this.Content = stackLayout;
        }

    }
}
