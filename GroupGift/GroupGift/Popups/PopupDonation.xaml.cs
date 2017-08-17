using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroupGift.Popups
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopupDonation : ContentView
	{
        public EventHandler CancelButtonEventHandler { get; set; }
        public EventHandler SaveButtonEventHandler { get; set; }

        public string DonationName { get; set; }
        public double DonationAmount { get; set; }
        public bool DonationIsReceived { get; set; }

        public PopupDonation ()
		{
            InitializeComponent();

            btnSave.Clicked += btnSave_Clicked;
            btnCancel.Clicked += btnCancel_Clicked;
            newDonationName.TextChanged += newDonationName_TextChanged;
            newDonationAmount.TextChanged += newDonationAmount_TextChanged;
            newDonationIsReceived.Toggled += newDonationIsReceived_Toggled;

            newDonationAmount.Keyboard = Keyboard.Numeric;
        }

        private void btnSave_Clicked(object sender, EventArgs e)
        {
            SaveButtonEventHandler?.Invoke(this, e);
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            CancelButtonEventHandler?.Invoke(this, e);
        }

        private void newDonationName_TextChanged(object sender, TextChangedEventArgs e)
        {
            DonationName = newDonationName.Text;
        }

        private void newDonationIsReceived_Toggled(object sender, ToggledEventArgs e)
        {
            DonationIsReceived = newDonationIsReceived.IsToggled;
        }

        private void newDonationAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            double d = 0;
            double.TryParse(newDonationAmount.Text, out d);
            DonationAmount = d;
        }
    }
}