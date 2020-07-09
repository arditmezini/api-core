using BookStore.Constants;
using BookStore.Contracts.Repository;
using BookStore.Contracts.Services.General;
using BookStore.Models.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BookStore.Repository
{
    public class GenericRepository : IGenericRepository
    {
        private static HttpClient client;
        private readonly HttpClientHandler httpClientHandler;
        private static JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver()
        };

        protected readonly IConnectionService _connectionService;
        protected readonly ISettingsService _settingsService;

        public GenericRepository(IConnectionService connectionService, ISettingsService settingsService)
        {
            _connectionService = connectionService;
            _settingsService = settingsService;

            httpClientHandler = new HttpClientHandler();
            if (httpClientHandler.SupportsAutomaticDecompression)
            {
                httpClientHandler.AutomaticDecompression =
                    DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }

#if DEBUG
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
#endif

            client = new HttpClient(httpClientHandler)
            {
                BaseAddress = new Uri(ApiConstants.BaseUrl)
            };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(ApiConstants.JsonMediaType));
            client.DefaultRequestHeaders.Add(ApiConstants.JwtAuthHeader, ApiConstants.JwtBarer + _settingsService.Token);
        }

        public async Task<T> Get<T>(string url)
        {
            try
            {
                if (!_connectionService.IsConnected)
                {
                    MessagingCenter.Send(MessagingConstants.RestApiClient, MessagingConstants.NoInternet);
                    return default;
                }

                var responseRequest = await client.GetAsync(url).ConfigureAwait(false);

                if (!responseRequest.IsSuccessStatusCode)
                    return default;

                var responseData = await responseRequest.Content.ReadAsStringAsync().ConfigureAwait(false);
                var response = JsonConvert.DeserializeObject<ApiResponse<T>>(responseData);
                return response.Result;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public async Task<TOut> Post<TOut, TIn>(string url, TIn content)
        {
            try
            {
                if (!_connectionService.IsConnected)
                {
                    MessagingCenter.Send(MessagingConstants.RestApiClient, MessagingConstants.NoInternet);
                    return default;
                }

                var serialized = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, ApiConstants.JsonMediaType);
                var responseRequest = await client.PostAsync(url, serialized).ConfigureAwait(false);

                if (!responseRequest.IsSuccessStatusCode)
                    return default;

                var responseData = await responseRequest.Content.ReadAsStringAsync().ConfigureAwait(false);
                var response = JsonConvert.DeserializeObject<ApiResponse<TOut>>(responseData, jsonSerializerSettings);
                return response.Result;
            }
            catch (Exception ex)
            {
                return default;
            }
        }
    }
}