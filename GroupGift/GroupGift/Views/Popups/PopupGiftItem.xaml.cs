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
        public EventHandler OKButtonEventHandler { get; set; }

        public string GiftItemGuid { get; set; }
        public string GiftItemName { get; set; }
        public double GiftItemPrice { get; set; }

        private bool _isDoDataChecks = false;

        public static readonly BindableProperty IsValidNameProperty = BindableProperty.Create("IsValidName", typeof(bool), typeof(PopupGiftItem), false, BindingMode.TwoWay);
        public bool IsValidName
        {
            get { return (bool)GetValue(IsValidNameProperty); }
            set { SetValue(IsValidNameProperty, value); }
        }

        public static readonly BindableProperty IsValidPriceProperty = BindableProperty.Create("IsValidPrice", typeof(bool), typeof(PopupGiftItem), false, BindingMode.TwoWay);
        public bool IsValidPrice
        {
            get { return (bool)GetValue(IsValidPriceProperty); }
            set { SetValue(IsValidPriceProperty, value); }
        }

        public PopupGiftItem()
        {
            Initialize();
            lblGiftItemHeader.Text = "Enter a new Gift Item";

            //initially set to true so errors are only shown after save click
            _isDoDataChecks = false;
            IsValidName = true;
            IsValidPrice = true;
        }

        public PopupGiftItem(GiftItem giftitem)
        {
            Initialize();
            lblGiftItemHeader.Text = "Update Gift Item";

            //set screen values
            GiftItemGuid = giftitem.Guid;
            eGiftName.Text = giftitem.Name;
            eGiftPrice.Text = giftitem.Price.ToString();

            //on initial load of screen, only do data checks on existing item
            _isDoDataChecks = true;
            DoDataChecks();
        }

        private void Initialize()
        {
            InitializeComponent();
            BindingContext = this;

            eGiftName.TextChanged += GiftName_TextChanged;
            eGiftPrice.TextChanged += GiftPrice_TextChanged;

            eGiftPrice.Keyboard = Keyboard.Numeric;
        }

        private void GiftName_TextChanged(object sender, TextChangedEventArgs e)
        {
            GiftItemName = eGiftName.Text;
            IsValidName = !string.IsNullOrEmpty(GiftItemName);
        }

        private void GiftPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            double d = 0;
            double.TryParse(eGiftPrice.Text, out d);
            GiftItemPrice = d;
            IsValidPrice = !string.IsNullOrEmpty(eGiftPrice.Text);
        }

        private void GiftOK_Tapped(object sender, EventArgs e)
        {
            _isDoDataChecks = true;
            DoDataChecks();
            if (IsValidName && IsValidPrice)
            {
                OKButtonEventHandler?.Invoke(this, e);
            }
        }

        private void GiftCancel_Tapped(object sender, EventArgs e)
        {
            CancelButtonEventHandler?.Invoke(this, e);
        }

        private void DoDataChecks()
        {
            if (_isDoDataChecks)
            {
                IsValidPrice = !string.IsNullOrWhiteSpace(eGiftPrice.Text);
                IsValidName = !string.IsNullOrWhiteSpace(eGiftName.Text);
            }
        }
    }
}