namespace BookStore.Contracts.Services.General
{
    public interface ISettingsService
    {
        string Username { get; set; }
        string Password { get; set; }
        bool RememberMe { get; set; }
        string Token { get; set; }
    }
}