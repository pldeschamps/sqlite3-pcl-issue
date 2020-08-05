using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using AlmicantaratXF.ViewModels;

namespace AlmicantaratXF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPageXAML : ContentPage
    {
        /*        public SettingsViewModel ViewModel
                {
                    get => this.BindingContext as SettingsViewModel;
                    set => this.BindingContext = value;
                }*/

        /// <summary>
        /// Capture de l'évènement Completed
        /// "Although you take a little stray from the MVVM pattern"
        ///  https://stackoverflow.com/questions/50260271/xamarin-detecting-enter-key-mvvm 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Entry_Completed(object sender, EventArgs e)
        {
#if DEBUG
            System.Diagnostics.Debug.WriteLine("dip " + ((Entry)sender).Text);
#endif
        }

        public SettingsPageXAML()
        {
            InitializeComponent();
            //            this.ViewModel = new SettingsViewModel();
            //BindingContext = new SettingsViewModel();
        }
        async void OnConstantCorrectionsClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new ConstantCorrectionsPage());
        }
    }
}