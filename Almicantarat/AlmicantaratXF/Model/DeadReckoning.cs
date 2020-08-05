using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Essentials;


namespace AlmicantaratXF.Model
{
    public static class DeadReckoning
    {
        public static double FixLat
        {
            get
            {
                return Preferences.Get("drFixLat", 0.0);
            }
            set
            {
                Preferences.Set("drFixLat", value);

            }
        }
        public static double FixLon
        {
            get
            {
                return Preferences.Get("drFixLon", 0.0);
            }
            set
            {

                Preferences.Set("drFixLon", value);
            }
        }

        public static DateTime FixDateTime
        {
            get
            {
                if (!Preferences.ContainsKey("drFixTime"))
                    Preferences.Set("drFixTime", DateTime.UtcNow);
                return Preferences.Get("drFixTime", DateTime.UtcNow).ToLocalTime();
            }
            set
            {
                Preferences.Set("drFixTime", value.ToUniversalTime());
            }
        }
        public static double Heading
        {
            get => Preferences.Get("drHeading", 0.0);
            set
            {
                Preferences.Set("drHeading", value);
            }
        }
        public static double SpeedKnots
        {
            get => Preferences.Get("drSpeed", 0.0);
            set
            {
                Preferences.Set("drSpeed", value);
            }
        }
        /// <summary>
        /// Get the current dead reckoning location
        /// </summary>
        /// <param name="dateTime">Local Time</param>
        /// <returns></returns>
    }
}
