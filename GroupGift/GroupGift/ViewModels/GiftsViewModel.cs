using GroupGift.Helpers;
using GroupGift.Models;
using GroupGift.Views;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GroupGift.ViewModels
{
    public class GiftsViewModel : BaseViewModel
	{
		public ObservableRangeCollection<GiftWrapper> Gifts { get; set; }
		public Command LoadGiftsCommand { get; set; }

        public GiftsViewModel()
        {
            try
            {
                Title = "Gifts";
                Gifts = new ObservableRangeCollection<GiftWrapper>();
                LoadGiftsCommand = new Command(async () => await ExecuteLoadItemsCommand());

                MessagingCenter.Subscribe<GiftDetailPage, GiftWrapper>(this, "SaveGift", async (obj, gift) =>
                {
                    try
                    {
                        gift.ConvertData();
                        await App.Database.SaveGiftAsync(gift.BaseGift);
                        await ExecuteLoadItemsCommand();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                });

                MessagingCenter.Subscribe<GiftDetailPage, GiftWrapper>(this, "ArchiveGift", async (obj, gift) =>
                {
                    try
                    {
                        gift.ConvertData();
                        await App.Database.SaveGiftAsync(gift.BaseGift);
                        await ExecuteLoadItemsCommand();
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                });

                MessagingCenter.Subscribe<GiftDetailPage, GiftWrapper>(this, "DeleteGift", async (obj, gift) =>
                {
                    try
                    {
                        gift.ConvertData();
                        await App.Database.DeleteGiftAsync(gift.BaseGift);
                        Gifts.Remove(gift);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }
                });

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

		async Task ExecuteLoadItemsCommand()
		{
			if (IsBusy) return;

			IsBusy = true;

            try
            {
                Gifts.Clear();
                var gifts = await App.Database.GetGiftsAynsc();
                foreach (var g in gifts)
                {
                    if (!g.IsArchived)
                    {
                        GiftWrapper gw = new GiftWrapper(g);
                        Gifts.Add(gw);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                MessagingCenter.Send(new MessagingCenterAlert { Title = "Error", Message = "Unable to load gifts.", Cancel = "OK" }, "message");
            }
            finally
            {
                IsBusy = false;
            }
		}

    }
}