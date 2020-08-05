using Android.App;
using Android.Content.PM;
using Android.Runtime;

using Android.OS;
using Xamarin.Forms.Internals;

namespace Almicantarat.Droid
{
	[Activity (Label = "Almicantarat", Theme = "@style/MainTheme", Icon = "@drawable/almicantarat_launcher", MainLauncher = true,
		ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	//Theme = "@style/MainTheme", 
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			Xamarin.Forms.Forms.SetFlags("SwipeView_Experimental");
			Xamarin.Essentials.Platform.Init(this, bundle);

			ToolbarResource = Resource.Layout.Toolbar;
			TabLayoutResource = Resource.Layout.Tabbar;

			Registrar.Registered.Register(typeof(Xamarin.Forms.CheckBox), typeof(Xamarin.Forms.Platform.Android.CheckBoxRenderer));

			global::Xamarin.Forms.Forms.Init (this, bundle);
            LoadApplication(new AlmicantaratXF.Views.App ());

        }
		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
		{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
	}
}

