using System;
using System.Collections.Generic;
using System.Text;
using System.Resources;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Globalization;
using SQLite;

using Xamarin.Essentials;
using static Xamarin.Essentials.Permissions;

using AlmicantaratXF.Resources;


namespace AlmicantaratXF.Model
{
    public enum PositionType : int { Unknown = 0, Satellite = 1, Celestial = 2, DeadReckoning = 3, Inertial = 4 }

    public class Position : INotifyPropertyChanged, IComparable<Position>
    {
        private Location location;

        ResourceManager rm = Strings.ResourceManager;
        public event PropertyChangedEventHandler PropertyChanged;

        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int? ID { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime DateTimeGroup { get; set; } //Currently local time ; TODO: convert to UTC
        //WARNING: TODO: change lines with DateTimeGroup in the method
        public PositionType PositionType { get; set; }
        public bool LocationPermission { get; set; }

        #region static methods
        private static double LastKnownLatitude
        {
            get => Preferences.Get("lastKnownLat", 0.0);
            set
            {
                Preferences.Set("lastKnownLat", value);
            }
        }
        private static double LastKnownLongitude
        {
            get => Preferences.Get("lastKnownLon", 0.0);
            set
            {
                Preferences.Set("lastKnownLon", value);
            }
        }
        public static Task<Position> BuildPositionAsync()
        {
            Position position = new Position();
            return position.InitializeLocationAsync();
        }
        #endregion
        public Position()
        {
            DateTimeGroup = DateTime.Now;
            PositionType = PositionType.Unknown;
            Latitude = 0;
            Longitude = 0;
            LocationPermission = false;
            OnPropertyChanged("StrLat");
            OnPropertyChanged("StrLon");
            OnPropertyChanged("StrDateTime");
        }
        public void CalculateCelestialPosition()
        {
            if(ID!=null)
            {
                List<Sight> sightList = AlmicantaratXF.Views.App.PositionsSightsDB.GetSightsList((int)ID);
                if(sightList.Count==1)
                {
                    //get the closest position on the sight circle to the assumed position
                    PositionType = PositionType.Celestial;
                    OnPropertyChanged("StrLat");
                    OnPropertyChanged("StrLon");
                    OnPropertyChanged("StrDateTime");
                }
                else if(sightList.Count==2)
                {
                    //get the closets intersection between the two circles to the assumed position
                }
                else if(sightList.Count>2)
                {
                    //Astrid method

                }
            }
        }
        private async Task<Position> InitializeLocationAsync()
        {
            Latitude = LastKnownLatitude;
            Longitude = LastKnownLongitude;
            OnPropertyChanged("StrLat");
            OnPropertyChanged("StrLon");
            if (LocationReference.Reference == LocationReference.deadReckoning)
            {
                DateTimeGroup = DateTime.Now;
                PositionType = PositionType.DeadReckoning;
                LastKnownLatitude = Latitude;
                LastKnownLongitude = Longitude;

            }
            else
            {
                try
                {
                    GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Lowest, TimeSpan.FromSeconds(3));

                    //Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                    //{
                        location = await GetLocationAsync(request);
                    //});
                    if (location != null)
                    {
                        Latitude = location.Latitude;
                        Longitude = -location.Longitude;
                        PositionType = PositionType.Satellite;
                        DateTimeGroup = location.Timestamp.DateTime;
                        LastKnownLatitude = Latitude;
                        LastKnownLongitude = Longitude;
                    }
                    else
                    {
                        Latitude = 0D;
                        Longitude = 0D;
                        PositionType = PositionType.Unknown;
                        DateTimeGroup = DateTime.Now;
                    }
                }
                catch (Exception ex)
                {
                    Latitude = 0D;
                    Longitude = 0D;
                    PositionType = PositionType.Unknown;
                    DateTimeGroup = DateTime.Now;
                }
            }
            OnPropertyChanged("StrLat");
            OnPropertyChanged("StrLon");
            OnPropertyChanged("StrDateTime");
            return this;
        }
        private async Task<Location> GetLocationAsync(GeolocationRequest request)
        { 
            var status = await CheckAndRequestPermissionAsync(new Permissions.LocationWhenInUse());
            if (status != PermissionStatus.Granted)
            {
                //await DisplayAlert("GPS", "You need to give permission to Almicantarat to get the device location", "OK");
                // Notify user permission was denied
                LocationPermission = false;
                return new Location(0,0);
            }
            else
            {
                Location location;
                //Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
                //{
                    location = await Geolocation.GetLocationAsync(request);
                //});
                LocationPermission = true;
                return location;
            }
        }
        private async Task<PermissionStatus> CheckAndRequestPermissionAsync<T>(T permission)
            where T : BasePermission
        {
            PermissionStatus status;
            //Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
            //{
                status = await permission.CheckStatusAsync();
            //});
            if (status != PermissionStatus.Granted)
            {
                status = await permission.RequestAsync();
            }

            return status;
        }

        #region identical to Sight class
        public string StrDateTime
        {
            get => DateTimeGroup.ToString();
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public int CompareTo(Position compared)
        {
            return this.DateTimeGroup.CompareTo(compared.DateTimeGroup); //TODO
        }
        #endregion
        public string StrMethod
        {
            get
            {
                switch (PositionType)
                {
                    case PositionType.Unknown:
                        return rm.GetString("positionTypeUnknown");
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
    }
}
