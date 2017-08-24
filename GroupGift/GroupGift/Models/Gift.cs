using System;
using System.Collections.ObjectModel;
using System.Linq;

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
            }
        }

        private bool _isCompleted = false;
        public bool IsCompleted {
            get { return _isCompleted; }
            set {
                SetProperty(ref _isCompleted, value);
            }
        }

        private bool _isFunded = false;
        public bool IsFunded
        {
            get { return _isFunded; }
            set
            {
                SetProperty(ref _isFunded, value);
            }
        }

        private bool _isArchived = false;
        public bool IsArchived
        {
            get { return _isArchived; }
            set
            {
                SetProperty(ref _isArchived, value);
            }
        }

        private bool _isReceived = false;
        public bool IsReceived
        {
            get { return _isReceived; }
            set { SetProperty(ref _isReceived, value); }
        }

        public string Items { get; set; }

        public string Donations { get; set; }

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

        public Gift()
        {
            IsFunded = false;
            IsCompleted = false;
            IsArchived = false;
        }

    }
}
