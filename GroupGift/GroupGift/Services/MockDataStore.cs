using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GroupGift.Models;

using Xamarin.Forms;
using System.Collections.ObjectModel;

[assembly: Dependency(typeof(GroupGift.Services.MockDataStore))]
namespace GroupGift.Services
{
	public class MockDataStore : IDataStore<Gift>
	{
		bool isInitialized;
        ObservableCollection<Gift> _gifts;

		public async Task<bool> AddItemAsync(Gift gift)
		{
			await InitializeAsync();

			_gifts.Add(gift);

			return await Task.FromResult(true);
		}

		public async Task<bool> UpdateItemAsync(Gift item)
		{
			await InitializeAsync();

			var _item = _gifts.Where((Gift arg) => arg.Id == item.Id).FirstOrDefault();
			_gifts.Remove(_item);
			_gifts.Add(item);

			return await Task.FromResult(true);
		}

		public async Task<bool> DeleteItemAsync(Gift item)
		{
			await InitializeAsync();

			var g = _gifts.Where((Gift arg) => arg.Id == item.Id).FirstOrDefault();
            _gifts.Remove(g);

			return await Task.FromResult(true);
		}

		public async Task<Gift> GetItemAsync(string id)
		{
			await InitializeAsync();

			return await Task.FromResult(_gifts.FirstOrDefault(s => s.Id == id));
		}

		public async Task<IEnumerable<Gift>> GetItemsAsync(bool forceRefresh = false)
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

            var ps = new ObservableCollection<Person>
            {
                new Person { Id = Guid.NewGuid().ToString(), FullName = "Samwise Gamgee", Email = "oldgaffer@shire.com" },
                new Person { Id = Guid.NewGuid().ToString(), FullName = "Bilbo Baggins", Email = "precious2@shire.com" },
                new Person { Id = Guid.NewGuid().ToString(), FullName = "Frodo Baggins", Email = "ringbearer@shire.com" },
                new Person { Id = Guid.NewGuid().ToString(), FullName = "Merriodac Brandywine", Email = "hobbitsfireworx@shire.com" },
                new Person { Id = Guid.NewGuid().ToString(), FullName = "Perrigrin Took", Email = "thefightinghobbit@shire.com" }
            };

            var gis = new ObservableCollection<GiftItem>
            {
                new GiftItem { Id=Guid.NewGuid().ToString(), Name = "Stroller", Price = 200.00 }
            };

            var pds = new ObservableCollection<PersonDonation>
            {
                new PersonDonation { Id=Guid.NewGuid().ToString(), PersonId = 1, Name="Samwise Gamgee", Amount = 20, IsReceived = true },
                new PersonDonation { Id=Guid.NewGuid().ToString(),PersonId = 2, Name="Bilbo Baggins", Amount = 50, IsReceived = true },
                new PersonDonation { Id=Guid.NewGuid().ToString(),PersonId = 3, Name="Frodo Baggins", Amount = 10, IsReceived = false },
                new PersonDonation { Id=Guid.NewGuid().ToString(),PersonId = 4, Name="Merriodas Brandybuck", Amount = 5, IsReceived = true },
                new PersonDonation { Id=Guid.NewGuid().ToString(),PersonId = 5, Name="Perrigrin Took", Amount = 20, IsReceived = false }
            };

            _gifts = new ObservableCollection<Gift>();
            _gifts.Add(new Gift { Id = Guid.NewGuid().ToString(), Name = "Baby Shower - Anne", Date = DateTime.Parse("08/22/2017"), IsCompleted = false, IsFunded = false, Items = gis, Donations = pds });
            _gifts.Add(new Gift { Id = Guid.NewGuid().ToString(), Name = "Tom's Retirement", Date = DateTime.Parse("07/11/2017"), IsCompleted = true, IsFunded = false, Items = gis, Donations = pds });
            _gifts.Add(new Gift { Id = Guid.NewGuid().ToString(), Name = "Mom's Birthday", Date = DateTime.Parse("04/08/2016"), IsCompleted = true, IsFunded = true, Items = gis, Donations = pds });

            foreach(Gift g in _gifts)
            {
                g.CalculateTotals();
            }

            isInitialized = true;
        }
	}
}
