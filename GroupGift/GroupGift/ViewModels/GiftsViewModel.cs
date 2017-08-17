using System;
using System.Diagnostics;
using System.Threading.Tasks;

using GroupGift.Helpers;
using GroupGift.Models;
using GroupGift.Views;

using Xamarin.Forms;

namespace GroupGift.ViewModels
{
	public class GiftsViewModel : BaseViewModel
	{
		public ObservableRangeCollection<Gift> Gifts { get; set; }
		public Command LoadGiftsCommand { get; set; }

		public GiftsViewModel()
		{
			Title = "Gifts";
			Gifts = new ObservableRangeCollection<Gift>();
            LoadGiftsCommand = new Command(async () => await ExecuteLoadItemsCommand());

			MessagingCenter.Subscribe<NewGiftPage, Gift>(this, "Add Gift", async (obj, gift) =>
			{
                var _gift = gift as Gift;
				Gifts.Add(_gift);
				await DataStore.AddItemAsync(_gift);
			});
		}

		async Task ExecuteLoadItemsCommand()
		{
			if (IsBusy) return;

			IsBusy = true;

            try
            {
                Gifts.Clear();
                var gifts = await DataStore.GetItemsAsync(true);
                Gifts.ReplaceRange(gifts);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert { Title = "Error", Message = "Unable to load gifts.", Cancel = "OK" }, "message");
            }
            finally
            {
                IsBusy = false;
            }
		}

    }
}