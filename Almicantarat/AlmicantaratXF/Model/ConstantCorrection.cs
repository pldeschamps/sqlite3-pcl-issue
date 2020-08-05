using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Globalization;
using SQLite;

using AlmicantaratXF.Data;
using AlmicantaratXF.ViewModels;
using AlmicantaratXF.Views;

namespace AlmicantaratXF.Model
{
    /// <summary>
    /// Sextant constant Correction in seconds at altitude At
    /// </summary>
    public class ConstantCorrection : INotifyPropertyChanged, IComparable<ConstantCorrection>
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }


        public ushort At { get; set; } //altitude (degrees)
        public int Correction { get; set; } // seconds

        public string StrAt
        {
            get => At.ToString("N0");
        }

        public string StrCorrection
        {
            get => Correction.ToString("N0");
            set
            {
                if (float.TryParse(value, NumberStyles.Float, CultureInfo.CurrentCulture, out float f_correction))
                {
                    if((float)Correction != f_correction)
                    {
                        Correction = (int)f_correction;
                        App.SettingsDB.SaveItemAsync(this);
                        //OnPropertyChanged("StrCorrection");
                    }
                }
            }
        }
        public ConstantCorrection()
        {
            this.At = 0;
            this.Correction = 0;
            OnPropertyChanged("StrCorrection");
        }
        public ConstantCorrection(ushort at, int correction)
        {
            this.At = at;
            this.Correction = correction;
            OnPropertyChanged("StrCorrection");
        }
        public ConstantCorrection(ushort at)
        {
            this.At = at;
            this.Correction = 0;
            OnPropertyChanged("StrCorrection");
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int CompareTo(ConstantCorrection compared)
        {
            return this.At.CompareTo(compared.At);
        }
    }
}
