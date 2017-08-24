using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace GroupGift.Models
{
    public class GiftWrapper : Gift
    {
        public Gift BaseGift
        {
            get
            {
                return new Gift
                {
                    AzureVersion = AzureVersion,
                    CreatedAt = CreatedAt,
                    Date = Date,
                    Donations = Donations,
                    Id = Id,
                    IsArchived = IsArchived,
                    IsCompleted = IsCompleted,
                    IsFunded = IsFunded,
                    Items = Items,
                    Name = Name,
                    TotalDonations = TotalDonations,
                    TotalDonationsReceived = TotalDonationsReceived,
                    TotalGiftPrice = TotalGiftPrice,
                    TotalRemainingAmt = TotalRemainingAmt,
                    TotalRemainingReceivedAmt = TotalRemainingReceivedAmt,
                    UpdatedAt = UpdatedAt
                };
            }
        }

        private string _dateDesc;
        public string DateDesc
        {
            get { return Date.ToString("dddd, MMMM d, yyyy"); }
            set { SetProperty(ref _dateDesc, value); }
        }

        public ObservableCollection<GiftItem> ItemsCollection { get; set; }

        public ObservableCollection<PersonDonation> DonationsCollection { get; set; }

        public void CalculateTotals()
        {
            //calc gift totals
            TotalGiftPrice = ItemsCollection.Sum(g => g.Price);
            //calc donation totals
            TotalDonations = DonationsCollection.Sum(x => x.Amount);
            TotalDonationsReceived = DonationsCollection.Where(x => x.IsReceived).Sum(y => y.Amount);
            TotalRemainingAmt = TotalGiftPrice - TotalDonations;
            if (TotalRemainingAmt < 0) { TotalRemainingAmt = 0; }
            TotalRemainingReceivedAmt = TotalGiftPrice - TotalDonationsReceived;
            if (TotalRemainingReceivedAmt < 0) { TotalRemainingReceivedAmt = 0; }
            //see if the gift is funded and if it's fully received
            if (TotalDonationsReceived >= TotalGiftPrice)
            {
                IsReceived = true;
                IsFunded = true;
            }
            else
            {
                IsReceived = false;
                if (TotalDonations >= TotalGiftPrice) { IsFunded = true; }
                else { IsFunded = false; }
            }
        }

        public GiftWrapper()
        {
            Initialize();
        }

        public GiftWrapper(Gift gift)
        {
            AzureVersion = gift.AzureVersion;
            CreatedAt = gift.CreatedAt;
            Date = gift.Date;
            Donations = gift.Donations;
            Id = gift.Id;
            IsArchived = gift.IsArchived;
            IsCompleted = gift.IsCompleted;
            IsFunded = gift.IsFunded;
            Items = gift.Items;
            Name = gift.Name;
            TotalDonations = gift.TotalDonations;
            TotalDonationsReceived = gift.TotalDonationsReceived;
            TotalGiftPrice = gift.TotalGiftPrice;
            TotalRemainingAmt = gift.TotalRemainingAmt;
            TotalRemainingReceivedAmt = gift.TotalRemainingReceivedAmt;
            UpdatedAt = gift.UpdatedAt;

            Initialize();
        }

        private void Initialize()
        {
            if (ItemsCollection == null)
            {
                if (!string.IsNullOrEmpty(Items))
                {
                    ItemsCollection = new ObservableCollection<GiftItem>(JsonConvert.DeserializeObject<List<GiftItem>>(Items));
                }
                else
                {
                    ItemsCollection = new ObservableCollection<GiftItem>();
                }
            }
            if (DonationsCollection == null)
            {
                if (!string.IsNullOrEmpty(Donations))
                {
                    DonationsCollection = new ObservableCollection<PersonDonation>(JsonConvert.DeserializeObject<List<PersonDonation>>(Donations));
                }
                else
                {
                    DonationsCollection = new ObservableCollection<PersonDonation>();
                }
            }
        }

        public void ConvertData()
        {
            Items = JsonConvert.SerializeObject(ItemsCollection);
            Donations = JsonConvert.SerializeObject(DonationsCollection);
        }

    }
}
