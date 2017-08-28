using GroupGift.Helpers;
using System.Collections.Generic;

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
            AboutVersion = string.Format(" Version {0}", "1.0.2");

            AboutLinkList = new ObservableRangeCollection<AboutLink>
            {
                new AboutLink { Text = "FAQs and Help", Link = "http://joecombs.com/projects/groupgift/" },
                new AboutLink { Text = "Rate this App on Google Play", Link = "https://play.google.com/store/apps/details?id=com.GroupGift" },
                new AboutLink { Text = "Send Feedback", Link = "mailto:dev@joecombs.com?Subject=Group Gift Feedback" }
            };

        }

    }
}
