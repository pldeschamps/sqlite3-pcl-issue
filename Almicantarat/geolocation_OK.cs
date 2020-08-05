using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;

using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Globalization;
using SQLite;

using Xamarin.Essentials;

using AlmicantaratSharedProject.MathTools;
using AlmicantaratXF.Resources;

namespace AlmicantaratXF.Model
{
    public enum PositionType : int { Satellite = 0, Celestial = 1, DeadReckoning = 2, Inertial = 3 }

    public class Position : LatitudeLongitude, INotifyPropertyChanged, IComparable<Position>
    {
        ResourceManager rm = Strings.ResourceManager;
        public event PropertyChangedEventHandler PropertyChanged;

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime DateTimeGroup { get; set; }
        public PositionType PositionType { get; set; }
        public Position()
        {
            //DateTimeGroup = DateTime.Now;
            PositionType = PositionType.Satellite;
            DateTimeGroup = DateTime.Now;
            GetDeviceLocation();

        }
        async public void GetDeviceLocation()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                var location = await Geolocation.GetLocationAsync(request);

                if (location != null)
                {
                    base.SetLatitudeLongitude(location.Latitude, location.Longitude);
                    OnPropertyChanged("StrLat");
                    OnPropertyChanged("StrLon");
                }
            }
            catch (Exception ex)
            {
                // Unable to get location
                base.SetLatitudeLongitude(0D, 0D);
            }
        }
        public string StrDateTime
        {
            get => DateTimeGroup.ToString();
        }
        public string StrMethod
        {
            get
            {
                switch (PositionType)
                {
                    case PositionType.Satellite:
                        return rm.GetString("Satellite");
                    case PositionType.Celestial:
                        return rm.GetString("Celestial");
                    case PositionType.DeadReckoning:
                        return rm.GetString("DeadReckoning");
                    case PositionType.Inertial:
                        return rm.GetString("Inertial");
                    default:
                        return "";
                }
            }
        }
        public string StrLat
        {
            get => this.Lat.ToString();
        }
        public string StrLon
        {
            get => this.Lon.ToString();
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public int CompareTo(Position compared)
        {
            return this.DateTimeGroup.CompareTo(compared.DateTimeGroup); //TODO
        }
    }
}
