using BookStore.Contracts.Services.Data;
using BookStore.Contracts.Services.General;
using BookStore.Models.Response;
using BookStore.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BookStore.ViewModels
{
    public class AuthorViewModel : ViewModelBase
    {
        private readonly IAuthorService _authorService;

        private ObservableCollection<AuthorResponse> _authors;
        public ObservableCollection<AuthorResponse> Authors
        {
            get => _authors;
            set => SetProperty(ref _authors, value);
        }

        public AuthorViewModel(INavigationService navigationService, IDialogService dialogService, IAuthorService authorService)
            : base(navigationService, dialogService)
        {
            _authorService = authorService;
        }

        public override async Task InitializeAsync(object data)
        {
            Authors = new ObservableCollection<AuthorResponse>(await _authorService.GetAuthors());
        }
    }
}