using GroupGift.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(GroupGift.Data.MockDataStore))]
namespace GroupGift.Data
{
    public class MockDataStore : IDataStore<Gift>
    {
        bool isInitialized;
        ObservableCollection<Gift> _gifts;

        public async Task<bool> AddGiftAsync(Gift gift)
        {
            await InitializeAsync();

            _gifts.Add(gift);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateGiftAsync(Gift gift)
        {
            await InitializeAsync();

            var _gift = _gifts.Where((Gift arg) => arg.Id == gift.Id).FirstOrDefault();
            _gifts.Remove(_gift);
            _gifts.Add(gift);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteGiftAsync(Gift gift)
        {
            await InitializeAsync();

            var g = _gifts.Where((Gift arg) => arg.Id == gift.Id).FirstOrDefault();
            _gifts.Remove(g);

            return await Task.FromResult(true);
        }

        public async Task<Gift> GetGiftAsync(int Id)
        {
            await InitializeAsync();

            return await Task.FromResult(_gifts.FirstOrDefault(s => s.Id == Id));
        }

        public async Task<IEnumerable<Gift>> GetGiftsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();
            return await Task.FromResult(_gifts);
        }

        public Task<bool> PullLatestAsync()
        {
            return Task.FromResult(true);
        }

        public Task<bool> SyncAsync()
        {
            return Task.FromResult(true);
        }

        public async Task InitializeAsync()
        {
            if (isInitialized) return;

            //string path = Application.Context.FilesDir.Path;


            var ps = new ObservableCollection<Person>
            {
                new Person { FullName = "Samwise Gamgee", Email = "oldgaffer@shire.com" },
                new Person { FullName = "Bilbo Baggins", Email = "precious2@shire.com" },
                new Person { FullName = "Frodo Baggins", Email = "ringbearer@shire.com" },
                new Person { FullName = "Merriodac Brandywine", Email = "hobbitsfireworx@shire.com" },
                new Person { FullName = "Perrigrin Took", Email = "thefightinghobbit@shire.com" }
            };

            var gis = new ObservableCollection<GiftItem>
            {
                new GiftItem {Name = "Stroller", Price = 200.00 }
            };

            var pds = new ObservableCollection<PersonDonation>
            {
                new PersonDonation { Name="Samwise Gamgee", Amount = 20, IsReceived = true },
                new PersonDonation { Name="Bilbo Baggins", Amount = 50, IsReceived = true },
                new PersonDonation { Name="Frodo Baggins", Amount = 10, IsReceived = false },
                new PersonDonation { Name="Merriodas Brandybuck", Amount = 5, IsReceived = true },
                new PersonDonation { Name="Perrigrin Took", Amount = 20, IsReceived = false }
            };

            //_gifts = new ObservableCollection<Gift>();
            //_gifts.Add(new Gift { Name = "Baby Shower - Anne", Date = DateTime.Parse("08/22/2017"), IsCompleted = false, IsFunded = false, Items = gis, Donations = pds });
            //_gifts.Add(new Gift { Name = "Tom's Retirement", Date = DateTime.Parse("07/11/2017"), IsCompleted = true, IsFunded = false, Items = gis, Donations = pds });
            //_gifts.Add(new Gift { Name = "Mom's Birthday", Date = DateTime.Parse("04/08/2016"), IsCompleted = true, IsFunded = true, Items = gis, Donations = pds });

            foreach (Gift g in _gifts)
            {
                //g.CalculateTotals();
            }

            isInitialized = true;
        }
    }
}