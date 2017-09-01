using GroupGift.Helpers;
using GroupGift.Models;
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroupGift.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopupDonation : ContentView
    {
        public EventHandler CancelButtonEventHandler { get; set; }
        public EventHandler OKButtonEventHandler { get; set; }

        public string DonationGuid { get; set; }
        public string DonationName { get; set; }
        public string DonationEmail { get; set; }
        public string DonationPhone { get; set; }
        public double DonationAmount { get; set; }
        public bool DonationIsReceived { get; set; }

        public static readonly BindableProperty IsValidNameProperty = BindableProperty.Create("IsValidName", typeof(bool), typeof(PopupGiftItem), false, BindingMode.TwoWay);
        public bool IsValidName
        {
            get { return (bool)GetValue(IsValidNameProperty); }
            set { SetValue(IsValidNameProperty, value); }
        }

        public static readonly BindableProperty IsValidAmountProperty = BindableProperty.Create("IsValidAmount", typeof(bool), typeof(PopupGiftItem), false, BindingMode.TwoWay);
        public bool IsValidAmount
        {
            get { return (bool)GetValue(IsValidAmountProperty); }
            set { SetValue(IsValidAmountProperty, value); }
        }

        public PopupDonation()
        {
            Initialize();
            lblDonationHeader.Text = "Enter a new Donation";

            IsValidName = true;
            IsValidAmount = true;        
        }

        public PopupDonation(PersonDonation donation)
        {
            Initialize();
            lblDonationHeader.Text = "Update Donation";

            DonationGuid = donation.Guid;
            eDonationName.Text = donation.Name;
            eDonationAmount.Text = donation.Amount.ToString();
            eDonationEmail.Text = donation.Email;
            eDonationPhone.Text = donation.Phone;
            swDonationIsReceived.IsToggled = donation.IsReceived;

            //on initial load of screen, only do data checks on existing item
            DoDataChecks();
        }

        private void Initialize()
        {
            InitializeComponent();
            BindingContext = this;

            eDonationName.TextChanged += DonationName_TextChanged;
            eDonationEmail.TextChanged += DonationEmail_TextChanged;
            eDonationPhone.TextChanged += DonationPhone_TextChanged;
            eDonationAmount.TextChanged += DonationAmount_TextChanged;
            swDonationIsReceived.Toggled += DonationIsReceived_Toggled;

            eDonationAmount.Keyboard = Keyboard.Numeric;
            eDonationPhone.Keyboard = Keyboard.Numeric;
        }

        private void DonationOK_Tapped(object sender, EventArgs e)
        {
            OKButtonEventHandler?.Invoke(this, e);
        }

        private void DonationCancel_Tapped(object sender, EventArgs e)
        {
            CancelButtonEventHandler?.Invoke(this, e);
        }

        private void DonationName_TextChanged(object sender, TextChangedEventArgs e)
        {
            DonationName = eDonationName.Text;
            DoDataChecks();
        }

        private void DonationEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            DonationEmail = eDonationEmail.Text;
        }

        private void DonationPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            DonationPhone = eDonationPhone.Text;
        }

        private void DonationIsReceived_Toggled(object sender, ToggledEventArgs e)
        {
            DonationIsReceived = swDonationIsReceived.IsToggled;
        }

        private void DonationAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            double.TryParse(eDonationAmount.Text, out double d);
            DonationAmount = d;
            DoDataChecks();
        }

        private void DoDataChecks()
        {
            IsValidName = !string.IsNullOrWhiteSpace(DonationName);
            IsValidAmount = !string.IsNullOrWhiteSpace(eDonationAmount.Text);
        }

    }
}