using System;

namespace BookStore.Utility.AsyncCommands
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}