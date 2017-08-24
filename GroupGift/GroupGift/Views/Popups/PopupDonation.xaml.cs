using GroupGift.Models;
using System;
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

        public PopupDonation()
        {
            Initialize();

            lblDonationHeader.Text = "Enter a new Donation";
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
        }

        private void Initialize()
        {
            InitializeComponent();

            btnSave.Clicked += BtnSave_Clicked;
            btnCancel.Clicked += BtnCancel_Clicked;
            newDonationName.TextChanged += NewDonationName_TextChanged;
            newDonationEmail.TextChanged += NewDonationEmail_TextChanged;
            newDonationPhone.TextChanged += NewDonationPhone_TextChanged;
            newDonationAmount.TextChanged += NewDonationAmount_TextChanged;
            newDonationIsReceived.Toggled += NewDonationIsReceived_Toggled;

            newDonationAmount.Keyboard = Keyboard.Numeric;
            newDonationPhone.Keyboard = Keyboard.Numeric;
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            SaveButtonEventHandler?.Invoke(this, e);
        }

        private void BtnCancel_Clicked(object sender, EventArgs e)
        {
            CancelButtonEventHandler?.Invoke(this, e);
        }

        private void NewDonationName_TextChanged(object sender, TextChangedEventArgs e)
        {
            DonationName = newDonationName.Text;
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
        }

    }
}