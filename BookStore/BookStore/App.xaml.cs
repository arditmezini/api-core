using BookStore.Bootstrap;
using BookStore.Constants;
using BookStore.Contracts.Services.General;
using BookStore.Utility;
using DLToolkit.Forms.Controls;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BookStore
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            InitializeCustomComponents();

            InitializeApp();

            AsyncUtil.RunSync(() => InitializeNavigation());
        }

        private void InitializeCustomComponents()
        {
            FlowListView.Init();

            AppCenter.Start(AppCenterConstants.IOSKey + /* AppCenterConstants.UwpKey + */ AppCenterConstants.AndroidKey,
                typeof(Analytics), typeof(Crashes));
        }

        private void InitializeApp()
        {
            AppContainer.RegisterDependencies();

            RegisterDeviceInternetAlertCallback();
        }

        private void RegisterDeviceInternetAlertCallback()
        {
            var dialogService = AppContainer.Resolve<IDialogService>();
            MessagingCenter.Subscribe<string>(this, MessagingConstants.NoInternet, async (eventSender) =>
            {
                await dialogService.ShowDialog(MessagingConstants.NoInternetTitle, MessagingConstants.NoInternetMessage, MessagingConstants.Cancel);
            });
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