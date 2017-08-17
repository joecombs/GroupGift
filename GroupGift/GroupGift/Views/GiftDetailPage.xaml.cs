using GroupGift.Models;
using GroupGift.Popups;
using GroupGift.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace GroupGift.Views
{
    public partial class GiftDetailPage : ContentPage
    {
        GiftDetailViewModel viewModel;

        // Note - The Xamarin.Forms Previewer requires a default, parameterless constructor to render a page.
        public GiftDetailPage()
        {
            InitializeComponent();
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Save",                
                Command = new Command(async () =>
                {
                    await DisplayAlert("title", "saving new item", "OK");
                })
            });
            GiftDetailViewModel vm = new GiftDetailViewModel();
            BindingContext = this.viewModel = vm;
            Initialize();
        }

        public GiftDetailPage(GiftDetailViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = this.viewModel = viewModel;
            Initialize();
        }

        private void Initialize ()
        {
            ToolbarItems.Remove(tbiPlaceholder);
            UpdateListViewSettings();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as PersonDonation;
            if (item == null)
                return;

            //await Navigation.PushAsync(new GiftDetailPage(new PersonDonation(item)));

            // Manually deselect item
            lvDonations.SelectedItem = null;
        }

        #region GiftItem Stuff
    
        private async void btnAddGiftItem_Clicked(object sender, System.EventArgs e)
        {
            var result = await LaunchAddGiftPopup();
            //load add donation screen
            UpdateListViewSettings();
        }

        private async Task<string> LaunchAddGiftPopup()
        {
            var inputview = new PopupGiftAdd();
            var popup = new InputAlertDialogBase<string>(inputview);

            inputview.CancelButtonEventHandler += (inputsender, obj) =>
            {
                popup.PageClosedTaskCompletionSource.SetResult(null);
            };

            inputview.SaveButtonEventHandler += (inputsender, obj) =>
            {
                if (inputsender is PopupGiftAdd)
                {
                    PopupGiftAdd pga = (PopupGiftAdd)inputsender;
                    GiftItem gi = new GiftItem { Id = System.Guid.NewGuid().ToString(), Name = pga.GiftItemName, Price = pga.GiftItemPrice };
                    viewModel.Gift.Items.Add(gi);
                    viewModel.Gift.CalculateTotals();
                }
                popup.PageClosedTaskCompletionSource.SetResult(null);
            };

            // Push the page to Navigation Stack
            await PopupNavigation.PushAsync(popup);

            // await for the user to enter the text input
            var result = await popup.PageClosedTask;

            // Pop the page from Navigation Stack
            await PopupNavigation.PopAsync();

            // return user inserted text value
            return result;
        }

        private void btnGiftDelete_Clicked(object sender, System.EventArgs e)
        {
            var b = sender as Button;
            GiftItem gi = b.BindingContext as GiftItem;

            var vm = BindingContext as GiftDetailViewModel;
            vm?.RemoveGiftItemCommand.Execute(gi);
            UpdateListViewSettings();
        }

        #endregion

        #region Donation Stuff 

        private async void btnAddDonation_Clicked(object sender, System.EventArgs e)
        {

            var result = await LaunchAddDonationPopup();

            //load add donation screen
            UpdateListViewSettings();
        }

        private async Task<string> LaunchAddDonationPopup()
        {
            var inputview = new PopupDonation();
            var popup = new InputAlertDialogBase<string>(inputview);

            inputview.CancelButtonEventHandler += (inputsender, obj) =>
            {
                popup.PageClosedTaskCompletionSource.SetResult(null);
            };

            inputview.SaveButtonEventHandler += (inputsender, obj) =>
            {
                if (inputsender is PopupDonation)
                {
                    PopupDonation popd = (PopupDonation)inputsender;
                    PersonDonation pd = new PersonDonation { Id = System.Guid.NewGuid().ToString(), Name = popd.DonationName, Amount = popd.DonationAmount, IsReceived = popd.DonationIsReceived };
                    viewModel.Gift.Donations.Add(pd);
                    viewModel.Gift.CalculateTotals();
                }

                popup.PageClosedTaskCompletionSource.SetResult(null);
            };

            // Push the page to Navigation Stack
            await PopupNavigation.PushAsync(popup);

            // await for the user to enter the text input
            var result = await popup.PageClosedTask;

            // Pop the page from Navigation Stack
            await PopupNavigation.PopAsync();

            // return user inserted text value
            return result;
        }

        private void btnDonationDelete_Clicked(object sender, System.EventArgs e)
        {
            var b = sender as Button;
            PersonDonation pd = b.BindingContext as PersonDonation;

            var vm = BindingContext as GiftDetailViewModel;
            vm?.RemoveDonationCommand.Execute(pd);
            UpdateListViewSettings();
        }

        #endregion

        private void UpdateListViewSettings()
        {

            try
            {
                if (viewModel != null && viewModel.Gift != null)
                {
                    if (viewModel.Gift.Items != null)
                    {
                        if (viewModel.Gift.Items.Count == 0) { lvItems.HeightRequest = 5; }
                        else { lvItems.HeightRequest = (55 * viewModel.Gift.Items.Count) + (10 * viewModel.Gift.Items.Count); }
                    }
                    if (viewModel.Gift.Donations != null)
                    {
                        if (viewModel.Gift.Donations.Count == 0) { lvDonations.HeightRequest = 5; }
                        else { lvDonations.HeightRequest = (55 * viewModel.Gift.Donations.Count) + (10 * viewModel.Gift.Donations.Count); }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void swDonationReceived_Toggled(object sender, ToggledEventArgs e)
        {
            if (sender is Switch s)
            {
                PersonDonation pd = s.BindingContext as PersonDonation;
                var vm = BindingContext as GiftDetailViewModel;
                vm.Gift.CalculateTotals();
            }
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("title", "message", "OK");
        }
    }
}
