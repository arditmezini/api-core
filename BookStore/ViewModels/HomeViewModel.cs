using BookStore.Constants;
using BookStore.Contracts.Services.Data;
using BookStore.Contracts.Services.General;
using BookStore.Models.Response;
using BookStore.Utility.AsyncCommands;
using BookStore.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class HomeViewModel : ViewModelBase
    {
        private readonly IStatisticsService _statisticsService;

        #region Bindable Properties
        private ObservableCollection<StatisticsResponse> _statistics;
        public ObservableCollection<StatisticsResponse> Statistics
        {
            get => _statistics;
            set => SetProperty(ref _statistics, value);
        }
        #endregion

        public HomeViewModel(INavigationService navigationService, IDialogService dialogService,
            IStatisticsService statisticsService)
            : base(navigationService, dialogService)
        {
            _statisticsService = statisticsService;

            ItemTappedCommand = new AsyncCommand<StatisticsResponse>(OnItemTapped);
        }

        public override async Task InitializeAsync(object data)
        {
            Statistics = new ObservableCollection<StatisticsResponse>(await _statisticsService.GetStatistics());
        }

        #region Commands
        public IAsyncCommand<StatisticsResponse> ItemTappedCommand { get; set; }

        private async Task OnItemTapped(StatisticsResponse item)
        {
            switch (item.Description)
            {
                case MenuConstants.Authors:
                    await _navigationService.NavigateToAsync<AuthorViewModel>();
                    break;
            }
        }
        #endregion
    }
}