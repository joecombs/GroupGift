using GroupGift.Models;
using GroupGift.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace GroupGift.Views 
{
	public partial class GiftsView : ContentPage
	{
        GiftsViewModel viewModel;

        public GiftsView ()
		{
			InitializeComponent ();
            BindingContext = viewModel = new GiftsViewModel();
		}

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Gift;
            if (item == null)
                return;

            await Navigation.PushAsync(new GiftDetailPage(new GiftDetailViewModel(item)));

            // Manually deselect item
            GiftsListView.SelectedItem = null;
        }

        async void AddGift_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new GiftDetailPage());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Gifts.Count == 0)
                viewModel.LoadGiftsCommand.Execute(null);
        }
    }
}