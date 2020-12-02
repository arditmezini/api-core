using BookStore.Constants;
using BookStore.Contracts.Services.General;
using BookStore.Models.Hub;
using BookStore.Utility.AsyncCommands;
using BookStore.ViewModels.Base;
using Microsoft.AspNetCore.SignalR.Client;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BookStore.ViewModels
{
    public class NewsViewModel : ViewModelBase
    {
        private HubConnection hubConnection;
        private readonly ISettingsService _settingsService;

        #region Bindable Properties
        private ObservableCollection<NewsModel> _news;
        public ObservableCollection<NewsModel> News
        {
            get => _news;
            set => SetProperty(ref _news, value);
        }

        private bool _isConnected;
        public bool IsConnected
        {
            get => _isConnected;
            set => SetProperty(ref _isConnected, value);
        }
        #endregion

        public NewsViewModel(INavigationService navigationService, IDialogService dialogService, ISettingsService settingsService)
            : base(navigationService, dialogService)
        {
            _settingsService = settingsService;

            ConfigureNewsHub();
        }

        private void ConfigureNewsHub()
        {
            IsConnected = false;

            hubConnection = new HubConnectionBuilder()
                .WithUrl(HubConstants.NewsHubUrl, options =>
                {
                    options.AccessTokenProvider = () => Task.FromResult(_settingsService.Token);
                    options.HttpMessageHandlerFactory = (message) =>
                    {
                        if (message is HttpClientHandler clientHandler)
                            clientHandler.ServerCertificateCustomValidationCallback +=
                                (sender, certificate, chain, sslPolicyErrors) => { return true; };
                        return message;
                    };
                }).Build();

            hubConnection.On<IEnumerable<NewsModel>>(HubConstants.OpenNews, (news) =>
            {
                News = new ObservableCollection<NewsModel>(news);
            });

            hubConnection.On<NewsModel>(HubConstants.SendNews, (news) =>
            {
                News.Add(news);
            });

            hubConnection.On<IEnumerable<NewsModel>>(HubConstants.CloseNews, (news) =>
            {
                News = new ObservableCollection<NewsModel>(news);
            });

            MessagingCenter.Subscribe<string>(this, MessagingConstants.CloseNewsHub, async (eventSender) =>
            {
                await Disconnect();
            });
        }

        public override async Task InitializeAsync(object data)
        {
            await Connect();
        }

        async Task Connect()
        {
            await hubConnection.StartAsync();
            await hubConnection.InvokeAsync(HubConstants.OpenNews);

            IsConnected = true;
        }

        async Task Disconnect()
        {
            await hubConnection.InvokeAsync(HubConstants.CloseNews);
            await hubConnection.StopAsync();

            IsConnected = false;
        }
    }
}