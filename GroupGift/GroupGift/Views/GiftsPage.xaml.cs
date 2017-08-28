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

            ToolbarItems.Add(new ToolbarItem
            {
                Text = "FAQs",
                Order = ToolbarItemOrder.Secondary,
                Priority = 1,
                Command = new Command(() =>
                {
                    Device.OpenUri(new Uri("http://joecombs.com/projects/groupgift/"));
                })
            });

            ToolbarItems.Add(new ToolbarItem
            {
                Text = "About",
                Order = ToolbarItemOrder.Secondary,
                Priority = 2,
                Command = new Command(async () =>
                {
                    await Navigation.PushAsync(new AboutPage());
                })
            });
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

        private async void Gifts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.LoadGiftsCommand.Execute(null);
        }

    }
}