using Acr.UserDialogs;
using BookStore.Contracts.Services.General;
using System;
using System.Threading.Tasks;

namespace BookStore.Services.General
{
    public class DialogService : IDialogService
    {
        public Task ShowDialog(string title, string message, string buttonText)
            => UserDialogs.Instance.AlertAsync(message, title, buttonText);

        public void ShowToast(string message, TimeSpan? timeSpan = null)
            => UserDialogs.Instance.Toast(message, timeSpan);
    }
}