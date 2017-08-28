using Xamarin.Forms;
using System.Collections.Generic;
using System;
using System.Diagnostics;
using GroupGift.ViewModels;
using static GroupGift.ViewModels.AboutViewModel;

namespace GroupGift.Views
{
    public partial class AboutPage : ContentPage
    {
        AboutViewModel viewModel;

        public AboutPage()
        {
            try
            {
                InitializeComponent(); 
                BindingContext = viewModel = new AboutViewModel();
            }
            catch(Exception ex )
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void LabelAboutLink_Tapped(object sender, EventArgs e)
        {
            Label l = (Label)sender;
            AboutLink al = (AboutLink)l.BindingContext;
            Device.OpenUri(new Uri(al.Link));
        }

        private void ButtonAboutLink_Clicked(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            AboutLink al = (AboutLink)b.BindingContext;
            Device.OpenUri(new Uri(al.Link));
        }
    }
}
