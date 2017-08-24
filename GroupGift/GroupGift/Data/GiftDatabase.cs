using GroupGift.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GroupGift.Data
{
    public class GiftDatabase
    {
        readonly SQLiteAsyncConnection database;

        public GiftDatabase(string path)
        {
            try
            {
                database = new SQLiteAsyncConnection(path);
                database.CreateTableAsync<Gift>().Wait();
            }
            catch (SQLiteException se)
            {
                Debug.WriteLine(se.Message);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public Task<List<Gift>> GetGiftsAynsc()
        {
            return database.Table<Gift>().ToListAsync();
        }

        public Task<Gift> GetGiftAsync(int id)
        {
            return database.Table<Gift>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveGiftAsync(Gift gift)
        {
            try
            {
                if (gift.Id == 0 || gift.Id == -1) { return database.InsertAsync(gift); }
                else { return database.UpdateAsync(gift); }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        public Task<int> DeleteGiftAsync(Gift gift)
        {
            try
            {
                return database.DeleteAsync(gift);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
