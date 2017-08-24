using Xamarin.Forms;
using System.Collections.Generic;
using System;

namespace GroupGift.Views
{
    public partial class AboutPage : ContentPage
    {
        public class AboutLinks
        {
            public string Text { get; set; }
            public string Link { get; set; }
        }

        public List<AboutLinks> AboutLinkList;

        public AboutPage()
        {
            InitializeComponent();
            lblVersion.Text = string.Format("Version {0}", "1.0.0");

            AboutLinkList.Add(new AboutLinks { Text = "Rate this App on Google Play", Link = "http://play.google.com/store/apps/details?id=com.google.android.apps.maps" });
            AboutLinkList.Add(new AboutLinks { Text = "Send Feedback", Link = "mailto:joe@joecombs.com" });
        }

        private void Button_Clicked(object sender, System.EventArgs e)
        {
            Device.OpenUri(new Uri("mailto:joe@joecombs.com"));
        }
    }
}
