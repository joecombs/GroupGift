using GroupGift.Models;
using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace GroupGift.ViewModels
{
    public class GiftDetailViewModel : BaseViewModel
	{
        private GiftWrapper _gift;
        public GiftWrapper Gift
        {
            get { return _gift; }
            set { SetProperty(ref _gift, value); }
        }

        public GiftDetailViewModel(GiftWrapper gift = null)
		{
            try
            {
                if (gift == null)
                {
                    //create new object and default to today's date
                    gift = new GiftWrapper()
                    {
                        Date = DateTime.Parse(DateTime.Now.ToString("MM/dd/yyyy"))
                    };
                }

                Title = gift.Name;
                Gift = gift;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
		}

        public Command<GiftItem> RemoveGiftItemCommand
        {
            get
            {
                return new Command<GiftItem>((giftitem) =>
                {
                    Gift.ItemsCollection.Remove(giftitem);
                    Gift.CalculateTotals();
                });
            }
        }

        public Command<PersonDonation> RemoveDonationCommand
        {
            get
            {
                return new Command<PersonDonation>((donation) =>
                {
                    Gift.DonationsCollection.Remove(donation);
                    Gift.CalculateTotals();
                });
            }
        }
    }
}