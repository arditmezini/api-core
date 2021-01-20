using BookStore.Constants;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookStore.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsView : ContentPage
    {
        public NewsView()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            MessagingCenter.Send(MessagingConstants.NewsHub, MessagingConstants.CloseNewsHub);
        }
    }
}