using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using AlmicantaratXF.Model;
using System.IO;

namespace AlmicantaratXF.Data
{
    public class SettingsDatabase
    {
        readonly SQLiteAsyncConnection database;

        private List<ConstantCorrection> constantCorrections { get; set; }

        public SettingsDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<ConstantCorrection>().Wait();
            constantCorrections = database.Table<ConstantCorrection>().ToListAsync().Result;
            if (constantCorrections.Count<9)
            {
                database.DeleteAllAsync<ConstantCorrection>();
                for (ushort at = 10; at < 100; at += 10)
                {
                    database.InsertAsync(new ConstantCorrection(at));
                }
            }
        }
        public List<ConstantCorrection> GetListCorrectionsSorted()
        {
            List<ConstantCorrection> sortedCorrections;
            sortedCorrections = database.Table<ConstantCorrection>().ToListAsync().Result;
            sortedCorrections.Sort();
            return sortedCorrections;
        }
        public async Task<ObservableCollection<ConstantCorrection>> GetCorrectionsSortedAsync()
        {
            List<ConstantCorrection> sortedCorrections;
            sortedCorrections = await database.Table<ConstantCorrection>().ToListAsync();
            sortedCorrections.Sort();
            return new ObservableCollection<ConstantCorrection>(sortedCorrections);
        }
        public async Task<List<ConstantCorrection>> GetListCorrectionsSortedAsync()
        {
            List<ConstantCorrection> sortedCorrections;
            sortedCorrections = await database.Table<ConstantCorrection>().ToListAsync();
            sortedCorrections.Sort();
            return sortedCorrections;
        }
        public Task<ConstantCorrection> GetItemAsync(int id)
        {
            return database.Table<ConstantCorrection>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(ConstantCorrection item)
        {
            if (item.ID != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }
        public void Clear()
        {
            database.DeleteAllAsync<ConstantCorrection>();
        }
/*
        public Task<int> DeleteItemAsync(ConstantCorrection item)
        {
            return database.DeleteAsync(item);
        }
*/
    }
}
