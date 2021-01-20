using Acr.UserDialogs;
using BookStore.Constants;
using System;
using System.Threading.Tasks;

namespace BookStore.Contracts.Services.General
{
    public interface IDialogService
    {
        Task ShowDialog(string title, string message, string buttonText);
        void ShowToast(string message, TimeSpan? timeSpan = null);
        void ShowLoading(string title = DialogConstants.Loading, MaskType type = MaskType.Black);
        void HideLoading();
    }
}