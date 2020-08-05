using System;
using System.Collections.Generic;
using System.Text;

using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Globalization;
using SQLite;

using Xamarin.Essentials;

using AlmicantaratXF.Data;
using AlmicantaratXF.ViewModels;

using AlmicantaratXF.Views;
using Xamarin.Forms;

#if DEBUG
using System.Diagnostics;
#endif

namespace AlmicantaratXF.Model
{
    public class Sight : INotifyPropertyChanged, IComparable<Sight>
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int? PositionID { get; set; } //The PositionID of the Position calculated with this Sight among other Sights
        public DateTime UTC { get; set; }
        public PositionType PositionType { get; set; }
        public double Latitude { get; set; } //The estimated position of the observer during the sight
        public double Longitude { get; set; }
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private int starID;
        public int StarID
        {
            get
            { return starID; }
            set
            {
                if(value!= starID)
                {
                    starID = value;
                    OnPropertyChanged(nameof(StarID));
                }
            }
        }

        public bool LowerLimb { get;
            set; }
        public float Altitude { get; set; } // Degrees
        public float IndexError { get; set; } // Minutes
        public float ConstantCorrection { get; set; } // Minutes
        public float DipCorrection { get; set; } // Minutes
        public float Temperature { get; set; } //°C
        public float Pressure { get; set; } // mbar = hPa
        public float Refraction { get; set; } // Minutes
        public float SemiDiameter { get; set; } // Degrees
        public float Parallax { get; set; } // Degrees
        public float TrueAltitude { get; set; } // Degrees
        private float oAltitude; // Observed Altitude
        public float ComputedAzimuthDeg { get; set; }
        public float ComputedAltitudeDeg { get; set; }
        public float InterceptNauticalMiles { get; set; }
        public double Declination { get; set; }
        public double GHA { get; set; }


        private Sight(int? positionID, Location location)
        {
            if (location != null)
            {
                Latitude = location.Latitude;
                Longitude = -location.Longitude;
            }
            else
            {
                Latitude = 0D;
                Longitude = 0D;
            }
            SightCommonConstructor(positionID);
        }
        /// <summary>
        /// Copy all datas but ID
        /// </summary>
        /// <param name="copiedSight"></param>
        public void Copy(Sight copiedSight)
        {
            PositionID = copiedSight.PositionID;
            UTC = copiedSight.UTC;
            PositionType = copiedSight.PositionType;
            Latitude = copiedSight.Latitude;
            Longitude = copiedSight.Longitude;
            StarID = copiedSight.StarID;
            LowerLimb = copiedSight.LowerLimb;
            Altitude = copiedSight.Altitude;
            IndexError = copiedSight.IndexError;
            ConstantCorrection = copiedSight.ConstantCorrection;
            DipCorrection = copiedSight.DipCorrection;
            Temperature = copiedSight.DipCorrection;
            Pressure = copiedSight.Pressure;
            Refraction = copiedSight.Refraction;
            SemiDiameter = copiedSight.SemiDiameter;
            Parallax = copiedSight.Parallax;
            TrueAltitude = copiedSight.TrueAltitude;
        }
        public Sight(int? positionID)
        {
            Latitude = 0D;
            Longitude = 0D;
            SightCommonConstructor(positionID);

        }
        public Sight()
        {
            Latitude = 0D;
            Longitude = 0D;
            SightCommonConstructor(0);
        }
        private void SightCommonConstructor(int? positionID)
        {
            PositionID = positionID;
            PositionType = PositionType.Unknown;
            UTC = DateTime.Now.ToUniversalTime();
            StarID = 0;
            LowerLimb = true;
            Altitude = 0;
            IndexError = 0;
            ConstantCorrection = 0;
            DipCorrection = 0;
            Temperature = 20;
            Pressure = 1013;
            Refraction = 0;
            SemiDiameter = 0;
            Parallax = 0;
            TrueAltitude = 0;
        }
        async public static System.Threading.Tasks.Task<Sight> BuildSightAsync(int? positionID)
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                var location = await Geolocation.GetLocationAsync(request);
                return new Sight(positionID, location);
            }
            catch (Exception ex)
            {
                return new Sight(positionID);
            }
        }
        #region identical to Position class
        public string StrDateTime
        {
            get => UTC.ToString();
        }
        public string StrComputedAzimuth
        {
            get => ComputedAzimuthDeg.ToString("000.0", System.Globalization.CultureInfo.CurrentCulture);
        }
        public string StrInterceptNM
        {
            get => InterceptNauticalMiles.ToString("+0.0;-0.0", System.Globalization.CultureInfo.CurrentCulture);
        }
        #endregion
        public int CompareTo(Sight compared)
        {
            return this.UTC.CompareTo(compared.UTC); //TODO
        }
    }
}
