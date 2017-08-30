using GroupGift.Helpers;
using GroupGift.Views;
using System.Collections.Generic;
using System.Reflection;

namespace GroupGift.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public class AboutLink
        {
            public string Text { get; set; }
            public string Link { get; set; }
        }

        public ObservableRangeCollection<AboutLink> AboutLinkList { get; set; }

        public string AboutAppTitle { get; set; }
        public string AboutVersion { get; set; }

        public AboutViewModel()
        {
            Title = "About";
            AboutAppTitle = "Group Gift";
            var v = App.Current.Properties.ToString();

            AppBuildVersion();

            AboutLinkList = new ObservableRangeCollection<AboutLink>
            {
                new AboutLink { Text = "FAQs and Help", Link = "http://joecombs.com/projects/groupgift/" },
                new AboutLink { Text = "Rate this App on Google Play", Link = "https://play.google.com/store/apps/details?id=com.GroupGift" },
                new AboutLink { Text = "Send Feedback", Link = "mailto:dev@joecombs.com?Subject=Group Gift Feedback" }
            };

        }

        private void AppBuildVersion()
        {
            // get app assembly attributes
            var assembly = typeof(AboutPage).GetTypeInfo().Assembly;
            // get name and version from assembly
            string appnameandversion = assembly.FullName;
            // split string to get the version string
            string[] splitString = appnameandversion.Split(',');
            // get the string with the version number
            string splitIntoVersion = splitString[1];
            // split the string into version and number components
            string[] numversion = splitIntoVersion.Split('=');
            // get the build version number n.n.n.n format
            string version = numversion[1];
            // display build version number
            AboutVersion = string.Format(" Version {0}", version);
        }

    }
}
