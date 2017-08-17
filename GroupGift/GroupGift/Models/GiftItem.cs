namespace GroupGift.Models
{
    public class GiftItem : BaseDataObject
    {
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
