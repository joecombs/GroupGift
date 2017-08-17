using System;
using GroupGift.Models;
using Xamarin.Forms;

namespace GroupGift.Views
{
	public partial class NewGiftPage : ContentPage
	{
		public Gift Gift { get; set; }

		public NewGiftPage()
		{
			InitializeComponent();

            Gift = new Gift
            {
                Name = "New Gift",
                IsCompleted = false,
                IsFunded = false,
                IsArchived = false
            };

			BindingContext = this;
		}

		async void Save_Clicked(object sender, EventArgs e)
		{
			MessagingCenter.Send(this, "AddGift", Gift);
			await Navigation.PopToRootAsync();
		}
	}
}