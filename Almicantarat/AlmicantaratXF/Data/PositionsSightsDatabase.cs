using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using AlmicantaratXF.Model;
using System.IO;
using System.Collections.ObjectModel;

namespace AlmicantaratXF.Data
{
    public class PositionsSightsDatabase
    {
        readonly SQLiteAsyncConnection database;

        public PositionsSightsDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Position>().Wait();
            database.CreateTableAsync<Sight>().Wait();
        }
        public ObservableCollection<Sight> GetSightsCollection(Position position)
        {
            if (position.ID != null)
            {
                List<Sight> sightsList = GetSightsList((int)position.ID);
                ObservableCollection<Sight> sightsCollection = new ObservableCollection<Sight>(sightsList);
                return sightsCollection;

            }
            else
                //returns an empty List
                return new ObservableCollection<Sight>();
        }
        public List<Sight> GetSightsList(int ID) => database.Table<Sight>().Where(i => i.PositionID == ID).ToListAsync().Result;
        public Task<List<Position>> GetPositionsAsync()
        {
            return database.Table<Position>().ToListAsync();
        }
        public ObservableCollection<Position> GetPositionsSort()
        {
            List<Position> sortedPositions;
            sortedPositions = database.Table<Position>().ToListAsync().Result;
            sortedPositions.Sort();
            return new ObservableCollection<Position>(sortedPositions);
        }
        public Task<Sight> GetOneSightAsync(int id)
        {
            return database.Table<Sight>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveSightAsync(Sight item)
        {
            if (item.ID != 0 && item.ID != null)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
        public Task<int> SavePositionAsync(Position item)
        {
            if (item.ID != 0 && item.ID != null)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteSightAsync(Sight item)
        {
            return database.DeleteAsync(item);
        }
        public void DeleteSights(int? positionId)
        {
            if (positionId != null)
            {
                List<Sight> sightsList = database.Table<Sight>().Where(i => i.PositionID == positionId).ToListAsync().Result;
                foreach (Sight item in sightsList)
                {
                    database.DeleteAsync(item);
                }
            }
        }
    public Task<int> DeletePositionAsync(Position item)
        {
            return database.DeleteAsync(item);
        }

    }
}
