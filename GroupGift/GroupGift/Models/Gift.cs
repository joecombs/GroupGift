using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace GroupGift.Models
{
    public class Gift : BaseDataObject
    {
        private string _name = "";
        public string Name {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private DateTime _date;
        public DateTime Date {
            get { return _date; }
            set
            {
                SetProperty(ref _date, value);
                DateDesc = _date.ToLongDateString();
            }
        }

        private string _dateDesc;
        public string DateDesc
        {
            get { return _dateDesc; }
            set { SetProperty(ref _dateDesc, value); }
        }
        private bool _isCompleted = false;
        public bool IsCompleted {
            get { return _isCompleted; }
            set {
                SetProperty(ref _isCompleted, value);
                IsCompletedDesc = _isCompleted ? "Yes" : "No";
            }
        }

        private string _isCompletedDesc = "";
        public string IsCompletedDesc
        {
            get { return _isCompletedDesc; }
            set { SetProperty(ref _isCompletedDesc, value); }
        }

        private bool _isFunded = false;
        public bool IsFunded
        {
            get { return _isFunded; }
            set
            {
                SetProperty(ref _isFunded, value);
                IsFundedDesc = IsFunded ? "Yes" : "No";
            }
        }

        private string _isFundedDesc = "";
        public string IsFundedDesc
        {
            get { return _isFundedDesc; }
            set { SetProperty(ref _isFundedDesc, value); }
        }

        private bool _isArchived = false;
        public bool IsArchived
        {
            get { return _isArchived; }
            set
            {
                SetProperty(ref _isArchived, value);
                IsArchivedDesc = IsArchived ? "Yes" : "No";
            }
        }

        private string _isArchivedDesc = "";
        public string IsArchivedDesc
        {
            get { return _isArchivedDesc; }
            set { SetProperty(ref _isArchivedDesc, value); }
        }

        public ObservableCollection<GiftItem> Items { get; set; }

        public ObservableCollection<PersonDonation> Donations { get; set; }

        private double _totalGiftPrice = 0;

        public double TotalGiftPrice
        {
            get { return _totalGiftPrice; }
            set { SetProperty(ref _totalGiftPrice, value); }
        }

        private double _totalDonations = 0;
        public double TotalDonations {
            get { return _totalDonations; }
            set { SetProperty(ref _totalDonations, value); }
        }

        private double _totalDonationsReceived = 0;
        public double TotalDonationsReceived
        {
            get { return _totalDonationsReceived; }
            set { SetProperty(ref _totalDonationsReceived, value); }
        }

        private double _totalRemainingAmt = 0;
        public double TotalRemainingAmt
        {
            get { return _totalRemainingAmt; }
            set { SetProperty(ref _totalRemainingAmt, value); }
        }

        private double _totalRemainingReceivedAmt = 0;
        public double TotalRemainingReceivedAmt
        {
            get { return _totalRemainingReceivedAmt; }
            set { SetProperty(ref _totalRemainingReceivedAmt, value); }
        }

        public void CalculateTotals()
        {
            //calc gift totals
            TotalGiftPrice = Items.Sum(g => g.Price);
            //calc donation totals
            TotalDonations = Donations.Sum(x => x.Amount);
            TotalDonationsReceived = Donations.Where(x => x.IsReceived).Sum(y => y.Amount);
            TotalRemainingAmt = TotalGiftPrice - TotalDonations;
            TotalRemainingReceivedAmt = TotalGiftPrice - TotalDonationsReceived;
        }

        public Gift()
        {
            if (Items == null) { Items = new ObservableCollection<GiftItem>(); }
            if (Donations == null ) { Donations = new ObservableCollection<PersonDonation>(); }
        }

    }
}
