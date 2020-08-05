using System;

using System.Globalization;
using Xamarin.Essentials;

namespace AlmicantaratXF.Model
{
    public static class Settings
    {
        public static float Dip
        {
            get => Preferences.Get("dip", 4.0f);
            set
            {
                Preferences.Set("dip", value);
                //OnPropertyChanged("Dip_input");
            }
        }
        public static float IndexError
        {
            get => Preferences.Get("index_error", 0.0f);
            set
            {
                Preferences.Set("index_error", value);
            }
        }

        public static DateTime TaiUtcLastUpdate
        {
            get => Preferences.Get("TAI_UTC_last_update", DateTime.Parse("1/1/2017", CultureInfo.InvariantCulture));
            set
            {
                Preferences.Set("TAI_UTC_last_update", value);
            }
        }
        public static int TaiUtcSeconds
        {
            get => Preferences.Get("TAI_UTC_Seconds", 37);
            set
            {
                Preferences.Set("TAI_UTC_Seconds", value);
            }
        }
    }
}
