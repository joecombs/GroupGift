using GroupGift.Models;
using GroupGift.Views;
using System;
using System.Diagnostics;
using System.Windows.Input;
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

        public ICommand DeleteGiftItemCommand
        {
            get { return new Command<GiftItem>(DeleteGiftItem); }
        }

        private async void DeleteGiftItem(object obj)
        {
            GiftItem giftitem = obj as GiftItem;
            if (await Application.Current.MainPage.DisplayAlert("Delete Gift Item", "Are you sure you want to delete this gift item?", "Yes", "No"))
            {
                Gift.ItemsCollection.Remove(giftitem);
                Gift.CalculateTotals();
                MessagingCenter.Send(this, "DeleteGiftItem", giftitem);
            }
        }

        public ICommand DeleteDonationCommand
        {
            get { return new Command<PersonDonation>(DeleteDonation); }
        }

        private async void DeleteDonation(object obj)
        {
            PersonDonation donation = obj as PersonDonation;
            if (await Application.Current.MainPage.DisplayAlert("Delete Donation", "Are you sure you want to delete this Donation?", "Yes", "No"))
            {
                Gift.DonationsCollection.Remove(donation);
                Gift.CalculateTotals();
                MessagingCenter.Send(this, "DeleteDonation", donation);
            }
        }

    }
}