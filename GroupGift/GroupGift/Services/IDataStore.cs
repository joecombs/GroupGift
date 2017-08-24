using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroupGift.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddGiftAsync(T gift);
        Task<bool> UpdateGiftAsync(T gift);
        Task<bool> DeleteGiftAsync(T gift);
        Task<T> GetGiftAsync(int Id);
        Task<IEnumerable<T>> GetGiftsAsync(bool forceRefresh = false);

        Task InitializeAsync();
        Task<bool> PullLatestAsync();
        Task<bool> SyncAsync();
    }
}
