using GroupGift.Models;
using GroupGift.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroupGift.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GiftsPage : ContentPage
    {
        GiftsViewModel viewModel;

        public GiftsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new GiftsViewModel();
        }

        private async void AddGift_Clicked(object sender, EventArgs e)
        {
            try
            {
                await Navigation.PushAsync(new GiftDetailPage());
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private async void lvGifts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = e.SelectedItem as GiftWrapper;
                if (item == null) return;

                await Navigation.PushAsync(new GiftDetailPage(new GiftDetailViewModel(item)));
                lvGifts.SelectedItem = null;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadGiftsCommand.Execute(null);
        }

        /*

        private async void btnMenuAbout_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AboutPage());
            //Detail = new NavigationPage(new AboutPage());
            IsPresented = false;
        }
        */
    }
}