using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System.Globalization;
using Xamarin.Essentials;
using Xamarin.Forms;

using AlmicantaratXF.Model;

namespace AlmicantaratXF.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        public SettingsViewModel() { }
        public string Dip_input
        {
            get => Settings.Dip.ToString("N1", CultureInfo.CurrentCulture);
            set
            {
                if (float.TryParse(value, NumberStyles.Float, CultureInfo.CurrentCulture, out float dip))
                {
                    if (Settings.Dip != dip)
                    {
                        Settings.Dip = dip;
                    }
                }
                //TODO else { } ??
            }
        }
        public string Index_error_input //TODO Settings
        {
            get => Settings.IndexError.ToString("N1", CultureInfo.CurrentCulture);
            set
            {
                if (float.TryParse(value, NumberStyles.Float, CultureInfo.CurrentCulture, out float index_error))
                {
                    if (Settings.IndexError != index_error)
                    {
                        Settings.IndexError = index_error;
                    }
                }
            }
        }
        public DateTime TaiUtcLastUpdateInput  //TODO Settings
        {
            get => Settings.TaiUtcLastUpdate;
            set
            {
                    Settings.TaiUtcLastUpdate = value;
            }
        }
        public string TaiUtcSecondsInput  //TODO Settings
        {
            get =>Settings.TaiUtcSeconds.ToString("N0");
            set
            {
                if (float.TryParse(value, NumberStyles.Float, CultureInfo.CurrentCulture, out float f_taiUtc))
                {
                    if ((float)Settings.TaiUtcSeconds != f_taiUtc)
                    {
                        Settings.TaiUtcSeconds = (int)f_taiUtc;
                        OnPropertyChanged("TaiUtcSecondsInput");
                    }
                }
            }
        }

    }
}
