using GroupGift.Helpers;

namespace GroupGift.Models
{
    public class GiftItem : ObservableObject
    {
        private string _guid = "";
        public string Guid
        {
            get { return _guid; }
            set { SetProperty(ref _guid, value); }
        }

        private string _name = "";
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }


        private double _price = 0;
        public double Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }
    }
}
