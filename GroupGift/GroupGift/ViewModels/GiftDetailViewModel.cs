using GroupGift.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace GroupGift.ViewModels
{
	public class GiftDetailViewModel : BaseViewModel
	{
        private Gift _gift;
        public Gift Gift
        {
            get { return _gift; }
            set { SetProperty(ref _gift, value); }
        }

        public GiftDetailViewModel(Gift gift = null)
		{
            if (gift == null)
            {
                gift = new Gift();
            }

			Title = gift.Name;
			Gift = gift;
		}

        public Command<GiftItem> RemoveGiftItemCommand
        {
            get
            {
                return new Command<GiftItem>((giftitem) =>
                {
                    Gift.Items.Remove(giftitem);
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
                    Gift.Donations.Remove(donation);
                    Gift.CalculateTotals();
                });
            }
        }
    }
}