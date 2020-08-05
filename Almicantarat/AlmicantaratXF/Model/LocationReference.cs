using System;
using System.Collections.Generic;
using System.Text;

using Xamarin.Essentials;

namespace AlmicantaratXF.Model
{
    public enum LocationReferences
        {
            DeadReckoning=0,
            Device = 1
        }
    public static class LocationReference
    {
        public const bool deadReckoning = false;
        public const bool device = true;
        
        public static bool Reference
        {
            get
            {
                if (Preferences.Get("locationReference", 0) == (int)LocationReferences.DeadReckoning)
                    return deadReckoning;
                else
                    return device;
            }
            set
            {
                if (value)
                    Preferences.Set("locationReference", (int)LocationReferences.Device);
                else
                    Preferences.Set("locationReference", (int)LocationReferences.DeadReckoning);
            }
        }


    }
}
