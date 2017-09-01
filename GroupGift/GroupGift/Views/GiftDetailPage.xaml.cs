using GroupGift.Helpers;
using GroupGift.Models;
using GroupGift.ViewModels;
using GroupGift.Views.Popups;
using Plugin.Messaging;
using Rg.Plugins.Popup.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GroupGift.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GiftDetailPage : ContentPage
    {
        GiftDetailViewModel viewModel;

        public GiftDetailPage()
        {
            try
            {
                InitializeComponent();
                GiftDetailViewModel vm = new GiftDetailViewModel();
                BindingContext = this.viewModel = vm;
                Initialize();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public GiftDetailPage(GiftDetailViewModel viewModel)
        {
            try
            {
                InitializeComponent();
                BindingContext = this.viewModel = viewModel;
                Initialize();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void Initialize()
        {

            //always have save option, add archive and delete options if existing gift
            ToolbarItems.Add(new ToolbarItem
            {
                Text = "Save ",
                Order = ToolbarItemOrder.Primary,
                Priority = 0,
                Command = new Command(async () =>
                {
                    //calculate before save just in case
                    viewModel.Gift.CalculateTotals();
                    MessagingCenter.Send(this, "SaveGift", viewModel.Gift);
                    await Navigation.PopToRootAsync();
                })
            });

            if (viewModel == null || viewModel.Gift == null || viewModel.Gift.Id > 0)
            {
                ToolbarItems.Add(new ToolbarItem
                {
                    Text = "Archive",
                    Order = ToolbarItemOrder.Secondary,
                    Priority = 1,
                    Command = new Command(async () =>
                    {
                        var bContinue = await DisplayAlert("Archive Group Gift", "Are you sure you want to archive this Group Gift?", "Yes", "No");
                        if (bContinue)
                        {
                            viewModel.Gift.IsArchived = true;
                            MessagingCenter.Send(this, "ArchiveGift", viewModel.Gift);
                            await Navigation.PopToRootAsync();
                        }

                    })
                });


                ToolbarItems.Add(new ToolbarItem
                {
                    Text = "Delete",
                    Order = ToolbarItemOrder.Secondary,
                    Priority = 2,
                    Command = new Command(async () =>
                    {
                        var bContinue = await DisplayAlert("Delete Group Gift", "Are you sure you want to delete this Group Gift?", "Yes", "No");
                        if (bContinue)
                        {
                            MessagingCenter.Send(this, "DeleteGift", viewModel.Gift);
                            await Navigation.PopToRootAsync();
                        }

                    })
                });

            }

            UpdateListViewSettings();

            MessagingCenter.Subscribe<GiftDetailViewModel, PersonDonation>(this, "DeleteDonation", (obj, donation) =>
            {
                try
                {
                    UpdateListViewSettings();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });

            MessagingCenter.Subscribe<GiftDetailViewModel, GiftItem>(this, "DeleteGiftItem", (obj, donation) =>
            {
                try
                {
                    UpdateListViewSettings();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                }
            });
        }

        private void UpdateListViewSettings()
        {
            try
            {
                int iRowHeight = 25;
                if (viewModel != null && viewModel.Gift != null)
                {
                    if (viewModel.Gift.ItemsCollection != null)
                    {
                        if (viewModel.Gift.ItemsCollection.Count == 0) { lvItems.HeightRequest = 5; }
                        else { lvItems.HeightRequest = (iRowHeight * viewModel.Gift.ItemsCollection.Count) + (10 * viewModel.Gift.ItemsCollection.Count); }
                    }
                    if (viewModel.Gift.DonationsCollection != null)
                    {
                        if (viewModel.Gift.DonationsCollection.Count == 0) { lvDonations.HeightRequest = 5; }
                        else { lvDonations.HeightRequest = (iRowHeight * viewModel.Gift.DonationsCollection.Count) + (10 * viewModel.Gift.DonationsCollection.Count); }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        #region Gift Item stuff

        private async void btnAddGiftItem_Clicked(object sender, System.EventArgs e)
        {
            var result = await LaunchAddGiftPopup(null);
            UpdateListViewSettings();
        }

        private async Task<string> LaunchAddGiftPopup(GiftItem giftitem)
        {
            try
            {
                PopupGiftItem inputview;
                if (giftitem == null) { inputview = new PopupGiftItem(); }
                else { inputview = new PopupGiftItem(giftitem); }
                var popup = new InputAlertDialogBase<string>(inputview);

                inputview.CancelButtonEventHandler += (inputsender, obj) =>
                {
                    popup.PageClosedTaskCompletionSource.SetResult(null);
                };

                inputview.OKButtonEventHandler += (inputsender, obj) =>
                {
                    if (inputsender is PopupGiftItem popgi)
                    {
                        if (giftitem == null)
                        {
                            GiftItem gi = new GiftItem { Guid = Guid.NewGuid().ToString(), Name = popgi.GiftItemName, Price = popgi.GiftItemPrice };
                            viewModel.Gift.ItemsCollection.Add(gi);
                        }
                        else
                        {
                            GiftItem gi = viewModel.Gift.ItemsCollection.Where(x => x.Guid == popgi.GiftItemGuid).FirstOrDefault();
                            if (gi != null)
                            {
                                gi.Name = popgi.GiftItemName; gi.Price = popgi.GiftItemPrice;
                            }
                        }
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
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        private async void ListViewItems_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = e.SelectedItem as GiftItem;
                if (item == null) return;

                //need to show the popup to allow edit of values
                var result = await LaunchAddGiftPopup(item);

                lvItems.SelectedItem = null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        #endregion

        #region Donation Stuff

        private async void btnAddDonation_Clicked(object sender, EventArgs e)
        {
            var result = await LaunchDonationPopup(null);
            UpdateListViewSettings();
        }

        private async Task<string> LaunchDonationPopup(PersonDonation donation)
        {
            try
            {
                PopupDonation inputview;
                if (donation == null) { inputview = new PopupDonation(); }
                else { inputview = new PopupDonation(donation); }
                var popup = new InputAlertDialogBase<string>(inputview);

                inputview.CancelButtonEventHandler += (inputsender, obj) =>
                {
                    popup.PageClosedTaskCompletionSource.SetResult(null);
                };

                inputview.OKButtonEventHandler += (inputsender, obj) =>
                {
                    if (inputsender is PopupDonation popd)
                    {
                        if (donation == null)
                        {
                            PersonDonation pd = new PersonDonation { Guid = Guid.NewGuid().ToString(), Name = popd.DonationName, Email = popd.DonationEmail, Phone = popd.DonationPhone, Amount = popd.DonationAmount, IsReceived = popd.DonationIsReceived };
                            viewModel.Gift.DonationsCollection.Add(pd);
                        }
                        else
                        {
                            PersonDonation pd = viewModel.Gift.DonationsCollection.Where(x => x.Guid == popd.DonationGuid).FirstOrDefault();
                            if (pd != null)
                            {
                                pd.Name = popd.DonationName; pd.Email = popd.DonationEmail; pd.Phone = popd.DonationPhone; pd.Amount = popd.DonationAmount; pd.IsReceived = popd.DonationIsReceived;
                            }
                        }
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

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        private async void lvDonations_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as PersonDonation;
            if (item == null) return;

            //need to show the popup to allow edit of values
            var result = await LaunchDonationPopup(item);

            lvDonations.SelectedItem = null;
        }

        private void DonationReceived_Toggled(object sender, ToggledEventArgs e)
        {
            if (sender is Switch s)
            {
                PersonDonation pd = s.BindingContext as PersonDonation;
                var vm = BindingContext as GiftDetailViewModel;
                vm.Gift.CalculateTotals();
            }
        }

        #endregion

        private void DonationEmail_Clicked(object sender, EventArgs e)
        {
            try
            {
                MenuItem mi = sender as MenuItem;
                PersonDonation pd = mi.CommandParameter as PersonDonation;
                var semail = string.Format("mailto:{0}?Subject=Group Gift Reminder&Body=This is a reminder about the upcoming {1} Group Gift. You pledged to donate {2} to the gift.", pd.Email, viewModel.Gift.Name, pd.Amount);
                Device.OpenUri(new Uri(semail));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void DonationText_Clicked(object sender, EventArgs e)
        {
            try
            {
                MenuItem mi = sender as MenuItem;
                PersonDonation pd = mi.CommandParameter as PersonDonation;

                string msg = string.Format("{0}, this is a reminder that you pledged to donate {1} to {2}.", pd.Name, string.Format("{0:C2}", pd.Amount), viewModel.Gift.Name);
                CrossMessaging.Current.SmsMessenger.SendDonationSms(pd.Phone, msg);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void DonationCall_Clicked(object sender, EventArgs e)
        {
            MenuItem mi = sender as MenuItem;
            PersonDonation pd = mi.CommandParameter as PersonDonation;

            CrossMessaging.Current.PhoneDialer.MakeDonationCall(pd.Phone);
        }
    }
}