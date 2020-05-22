using BookStore.Bootstrap;
using BookStore.Contracts.Services.General;
using BookStore.Utility;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BookStore
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            InitializeApp();

            AsyncUtil.RunSync(() => InitializeNavigation());
        }

        private void InitializeApp()
        {
            AppContainer.RegisterDependencies();
        }

        private async Task InitializeNavigation()
        {
            var navigationService = AppContainer.Resolve<INavigationService>();
            await navigationService.InitializeAsync();
        }

        protected override void OnStart()
        { }

        protected override void OnSleep()
        { }

        protected override void OnResume()
        { }
    }
}
