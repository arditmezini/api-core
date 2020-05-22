using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BookStore.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BookStoreNavigationPage : NavigationPage
    {
        public BookStoreNavigationPage()
        {
            InitializeComponent();
        }

        public BookStoreNavigationPage(Page root) : base(root)
        {
            InitializeComponent();
        }
    }
}