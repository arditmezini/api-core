using BookStore.Contracts.Services.General;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace BookStore.Services.General
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettings _settings;

        public SettingsService()
        {
            _settings = CrossSettings.Current;
        }

        private void SetString(string key, string value)
        {
            _settings.AddOrUpdateValue(key, value);
        }

        private string GetString(string key)
        {
            return _settings.GetValueOrDefault(key, string.Empty);
        }

        public string Username
        {
            get => GetString(nameof(Username));
            set => SetString(nameof(Username), value);
        }

        public string Password
        {
            get => GetString(nameof(Password));
            set => SetString(nameof(Password), value);
        }

        public bool RememberMe
        {
            get => _settings.GetValueOrDefault(nameof(RememberMe), false);
            set => _settings.AddOrUpdateValue(nameof(RememberMe), value);
        }

        public string Token
        {
            get => GetString(nameof(Token));
            set => SetString(nameof(Token), value);
        }
    }
}