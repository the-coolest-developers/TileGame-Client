using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TileGameClient.Models;
using TileGameClient.Models.Requests;
using TileGameClient.Requests.SSO;

namespace TileGameClient.RequestSenders
{
    public class SsoRequestSender : ISsoRequestSender
    {
        private readonly IRequestSender _baseRequestSender;
        private readonly ISsoRequests _ssoRequests;

        public SsoRequestSender(ISsoRequests sso, IRequestSender baseSender)
        {
            _ssoRequests = sso;
            _baseRequestSender = baseSender;
        }

        public async Task<string> LogIn(LoginRequest request)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(request),
                _baseRequestSender.BaseConfiguration.Encoding,
                _baseRequestSender.BaseConfiguration.Content);

            var response = await _ssoRequests.LogIn(content);

            _baseRequestSender.BaseConfiguration.Token = response.Result.Token;
            return response.Result.Token;
        }

        public async Task<HttpStatusCode> Register(RegisterAccountRequest accountRequest)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(accountRequest),
                _baseRequestSender.BaseConfiguration.Encoding,
                _baseRequestSender.BaseConfiguration.Content);

            var res = await _ssoRequests.Register(content);

            return res;
        }

        public async Task<Account> GetAccount()
        {
            Account acc = null;
            if (_baseRequestSender.BaseConfiguration.Token != null)
            {
                if (_baseRequestSender.HttpClient.DefaultRequestHeaders.Authorization == null)
                    Authorize(_baseRequestSender.BaseConfiguration.Token);

                acc = await _ssoRequests.GetAccount();
            }

            return acc;
        }

        public void Authorize(string token) => _baseRequestSender.SetToken(token);
    }
}