using GroupGift.Models;
using System;
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

        public PopupGiftItem()
        {
            Initialize();
        }

        public PopupGiftItem(GiftItem giftitem)
        {
            Initialize();

            //set screen values
            GiftItemGuid = giftitem.Guid;
            eNewGiftName.Text = giftitem.Name;
            eNewGiftPrice.Text = giftitem.Price.ToString();
        }

        private void Initialize()
        {
            InitializeComponent();

            btnNewGiftSave.Clicked += btnNewGiftSave_Clicked;
            btnNewGiftCancel.Clicked += btnNewGiftCancel_Clicked;
            eNewGiftName.TextChanged += eNewGiftName_TextChanged;
            eNewGiftPrice.TextChanged += eNewGiftPrice_TextChanged;

            eNewGiftPrice.Keyboard = Keyboard.Numeric;
        }

        private void btnNewGiftSave_Clicked(object sender, EventArgs e)
        {
            SaveButtonEventHandler?.Invoke(this, e);
        }

        private void btnNewGiftCancel_Clicked(object sender, EventArgs e)
        {
            CancelButtonEventHandler?.Invoke(this, e);
        }

        private void eNewGiftName_TextChanged(object sender, TextChangedEventArgs e)
        {
            GiftItemName = eNewGiftName.Text;
        }

        private void eNewGiftPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            double d = 0;
            double.TryParse(eNewGiftPrice.Text, out d);
            GiftItemPrice = d;
        }

    }
}