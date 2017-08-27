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
        public EventHandler SaveButtonEventHandler { get; set; }

        public string DonationGuid { get; set; }
        public string DonationName { get; set; }
        public string DonationEmail { get; set; }
        public string DonationPhone { get; set; }
        public double DonationAmount { get; set; }
        public bool DonationIsReceived { get; set; }

        public static readonly BindableProperty IsDataValidProperty = BindableProperty.Create("IsDataValid", typeof(bool), typeof(PopupGiftItem), false, BindingMode.TwoWay);

        public bool IsDataValid
        {
            get { return (bool)GetValue(IsDataValidProperty); }
            set { SetValue(IsDataValidProperty, value); }
        }

        public PopupDonation()
        {
            Initialize();
            lblDonationHeader.Text = "Enter a new Donation";

            IsDataValid = bvRequiredName.IsValid && bvRequiredAmount.IsValid;
        }

        public PopupDonation(PersonDonation donation)
        {
            Initialize();
            lblDonationHeader.Text = "Update Donation";

            DonationGuid = donation.Guid;
            newDonationName.Text = donation.Name;
            newDonationAmount.Text = donation.Amount.ToString();
            newDonationEmail.Text = donation.Email;
            newDonationPhone.Text = donation.Phone;
            newDonationIsReceived.IsToggled = donation.IsReceived;

            IsDataValid = bvRequiredName.IsValid && bvRequiredAmount.IsValid;
        }

        private void Initialize()
        {
            InitializeComponent();
            BindingContext = this;

            newDonationName.TextChanged += NewDonationName_TextChanged;
            newDonationEmail.TextChanged += NewDonationEmail_TextChanged;
            newDonationPhone.TextChanged += NewDonationPhone_TextChanged;
            newDonationAmount.TextChanged += NewDonationAmount_TextChanged;
            newDonationIsReceived.Toggled += NewDonationIsReceived_Toggled;

            newDonationAmount.Keyboard = Keyboard.Numeric;
            newDonationPhone.Keyboard = Keyboard.Numeric;
        }

        private void lblNewDonationSave_Tapped(object sender, EventArgs e)
        {
            SaveButtonEventHandler?.Invoke(this, e);
        }

        private void lblNewDonationCancel_Tapped(object sender, EventArgs e)
        {
            CancelButtonEventHandler?.Invoke(this, e);
        }

        private void NewDonationName_TextChanged(object sender, TextChangedEventArgs e)
        {
            DonationName = newDonationName.Text;
            IsDataValid = bvRequiredName.IsValid && bvRequiredAmount.IsValid;
        }

        private void NewDonationEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            DonationEmail = newDonationEmail.Text;
        }

        private void NewDonationPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            DonationPhone = newDonationPhone.Text;
        }

        private void NewDonationIsReceived_Toggled(object sender, ToggledEventArgs e)
        {
            DonationIsReceived = newDonationIsReceived.IsToggled;
        }

        private void NewDonationAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            double.TryParse(newDonationAmount.Text, out double d);
            DonationAmount = d;
            IsDataValid = bvRequiredName.IsValid && bvRequiredAmount.IsValid;
        }

    }
}