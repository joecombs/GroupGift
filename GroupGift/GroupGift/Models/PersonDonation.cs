using System;

namespace GroupGift.Models
{
    public class PersonDonation : BaseDataObject
    {
        public long PersonId { get; set; }

        private string _name = "";
        public string Name {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private double _amount = 0;
        public double Amount {
            get { return _amount; }
            set { SetProperty(ref _amount, value); }
        }

        private bool _isReceived = false;
        public bool IsReceived {
            get { return _isReceived; }
            set {
                SetProperty(ref _isReceived, value);
                IsReceivedDesc = IsReceived ? "Yes" : "No";
            }
        }

        private string _isReceivedDesc = "";
        public string IsReceivedDesc
        {
            get { return _isReceivedDesc; }
            set { SetProperty(ref _isReceivedDesc, value); }
        }
    }
}
