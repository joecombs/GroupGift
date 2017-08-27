using GroupGift.Helpers;
using GroupGift.Models;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroupGift.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupGiftItem : ContentView
    {
        public EventHandler CancelButtonEventHandler { get; set; }
        public EventHandler SaveButtonEventHandler { get; set; }

        public string GiftItemGuid { get; set; }
        public string GiftItemName { get; set; }
        public double GiftItemPrice { get; set; }


        public static readonly BindableProperty IsDataValidProperty = BindableProperty.Create("IsDataValid", typeof(bool), typeof(PopupGiftItem), false, BindingMode.TwoWay);

        public bool IsDataValid
        {
            get { return (bool)GetValue(IsDataValidProperty); }
            set { SetValue(IsDataValidProperty, value); }
        }

        public PopupGiftItem()
        {
            Initialize();
            lblGiftItemHeader.Text = "Enter a new Gift Item";
        }

        public PopupGiftItem(GiftItem giftitem)
        {
            Initialize();
            lblGiftItemHeader.Text = "Update Gift Item";

            //set screen values
            GiftItemGuid = giftitem.Guid;
            eNewGiftName.Text = giftitem.Name;
            eNewGiftPrice.Text = giftitem.Price.ToString();
        }

        private void Initialize()
        {
            InitializeComponent();
            BindingContext = this;

            IsDataValid = false;

            eNewGiftName.TextChanged += eNewGiftName_TextChanged;
            eNewGiftPrice.TextChanged += eNewGiftPrice_TextChanged;

            eNewGiftPrice.Keyboard = Keyboard.Numeric;
        }

        private void eNewGiftName_TextChanged(object sender, TextChangedEventArgs e)
        {
            GiftItemName = eNewGiftName.Text;
            IsDataValid = bvRequiredName.IsValid && bvRequiredPrice.IsValid;
        }

        private void eNewGiftPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            double d = 0;
            double.TryParse(eNewGiftPrice.Text, out d);
            GiftItemPrice = d;
            IsDataValid = bvRequiredName.IsValid && bvRequiredPrice.IsValid;
        }

        private void lblNewGiftSave_Tapped(object sender, EventArgs e)
        {
            SaveButtonEventHandler?.Invoke(this, e);
        }

        private void lblNewGiftCancel_Tapped(object sender, EventArgs e)
        {
            CancelButtonEventHandler?.Invoke(this, e);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
    }
}