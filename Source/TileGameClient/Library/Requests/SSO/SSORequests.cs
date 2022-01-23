using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TileGameClient.Infrastructure.Responses;
using TileGameClient.Models;
using TileGameClient.Models.Configurations;
using TileGameClient.Models.Responses;
using TileGameClient.RequestSenders;

namespace TileGameClient.Requests.SSO
{
    public class SsoRequests : ISsoRequests
    {
        private readonly IRequestSender _baseSender;
        private readonly IBaseConfiguration _configuration;

        public SsoRequests(IRequestSender sender)
        {
            _baseSender = sender;
            _configuration = sender.BaseConfiguration;
        }

        public async Task<HttpStatusCodeResponse<LoginResponse>> LogIn(StringContent content)
        {
            var res = await _baseSender.Post(content, $"{_configuration.BaseSsoString}/login");

            string json = await res.Content.ReadAsStringAsync();
            var accountsLoginResponse = JsonConvert.DeserializeObject<LoginResponse>(json);

            return new HttpStatusCodeResponse<LoginResponse>()
            {
                Result = accountsLoginResponse,
                StatusCode = res.StatusCode
            };
        }

        public async Task<HttpStatusCode> Register(StringContent content)
        {
            var res = await _baseSender.Post(content, $"{_configuration.BaseSsoString}/register");

            return res.StatusCode;
        }

        public async Task<Account> GetAccount()
        {
            var res = await _baseSender.Get(_configuration.BaseSsoString + "/get");

            string json = await res.Content.ReadAsStringAsync();
            Account account = JsonConvert.DeserializeObject<Account>(json);

            return account;
        }
    }
}