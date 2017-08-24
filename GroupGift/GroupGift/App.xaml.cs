using GroupGift.Data;
using GroupGift.Helpers;
using GroupGift.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace GroupGift
{
    public partial class App : Application
    {
        static GiftDatabase _database;

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new GiftsPage());
        }

        public static GiftDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    _database = new GiftDatabase(DependencyService.Get<ILocalFileHelper>().GetLocalFilePath("GroupGiftDB.db3"));
                }
                return _database;
            }
        }
    }

}
