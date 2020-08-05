using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;

using System.Threading.Tasks;

using AlmicantaratXF.Model;
using AlmicantaratXF.Views;
using Xamarin.Forms;

namespace AlmicantaratXF.ViewModels
{
    public class PositionsViewModel : BaseViewModel
    {
        static public ObservableCollection<Position> Positions { get; set; }
        static private List<Position> ModifiedPositions { get; set; }

        public PositionsViewModel()
        {
            ModifiedPositions = new List<Position>();
            Positions = new ObservableCollection<Position>();
            Device.BeginInvokeOnMainThread(async () => await GetPositionsAsync());
            //Task.Run(async () => await GetPositionsAsync());
            Positions.CollectionChanged += this.OnCollectionChanged;
        }
        async Task GetPositionsAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var positions = await App.PositionsSightsDB.GetPositionsAsync();
                positions.Sort();
                Positions.Clear();
                foreach (var position in positions)
                    Positions.Add(position);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        public async Task<Position> GetNewAsyncPositionAsync()
        {
            Position newPosition = await Position.BuildPositionAsync();
            await AlmicantaratXF.Views.App.PositionsSightsDB.SavePositionAsync(newPosition);
            Positions.Add(newPosition);
            return newPosition;
        }
        public async Task<Position> GetNewPositionAsync()
        {
            Position newPosition = new Position();
            await AlmicantaratXF.Views.App.PositionsSightsDB.SavePositionAsync(newPosition);
            Positions.Add(newPosition);
            return newPosition;
        }
        void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems!= null)
            {
                foreach (Position newPosition in e.NewItems)
                {
                    ModifiedPositions.Add(newPosition);
                    if(e.Action == NotifyCollectionChangedAction.Add)
                        newPosition.PropertyChanged += OnPositionPropertyChanged;
                    else if (e.Action == NotifyCollectionChangedAction.Remove)
                        newPosition.PropertyChanged -= OnPositionPropertyChanged;
                }
            }
        }
        void OnPositionPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Position position = sender as Position;
            if (position != null)
                ModifiedPositions.Add(position);
        }
    }
}