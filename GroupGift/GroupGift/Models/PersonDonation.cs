using GroupGift.Helpers;
using System;

namespace GroupGift.Models
{
    public class PersonDonation : ObservableObject
    {
        private string _guid = "";
        public string Guid
        {
            get { return _guid; }
            set { SetProperty(ref _guid, value); }
        }

        private string _name = "";
        public string Name {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _email = "";
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private string _phone = "";
        public string Phone
        {
            get { return _phone; }
            set { SetProperty(ref _phone, value); }
        }

        private double _amount = 0;
        public double Amount {
            get { return _amount; }
            set { SetProperty(ref _amount, value); }
        }

        private bool _isReceived = false;
        public bool IsReceived
        {
            get { return _isReceived; }
            set { SetProperty(ref _isReceived, value); }
        }

    }
}
