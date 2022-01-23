using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TileGameClient.Infrastructure.Responses;
using TileGameClient.Models;
using TileGameClient.Models.Requests;
using TileGameClient.Models.Responses;
using TileGameClient.Requests.Player;

namespace TileGameClient.RequestSenders
{
    public class PlayerRequestSender : IPlayerRequestSender
    {
        private readonly IPlayerRequests _playerRequests;
        private readonly IRequestSender _baseRequestSender;

        public PlayerRequestSender(IPlayerRequests player, IRequestSender baseSender)
        {
            _playerRequests = player;
            _baseRequestSender = baseSender;
        }

        public async Task<HttpStatusCode> RegisterPlayer(RegisterPlayerRequest request)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(request),
                _baseRequestSender.BaseConfiguration.Encoding,
                _baseRequestSender.BaseConfiguration.Content);
            var res = await _playerRequests.Register(content);
            return res.StatusCode;
        }

        public async Task<HttpStatusCodeResponse<GetPlayerProfileResponse>> GetPlayerProfile()
        {
            var response = await _playerRequests.GetPlayerProfile();

            var body = await response.Content.ReadAsStringAsync();

            var getPlayerProfileResponse = JsonConvert.DeserializeObject<GetPlayerProfileResponse>(body);

            return new HttpStatusCodeResponse<GetPlayerProfileResponse>()
            {
                Result = getPlayerProfileResponse,
                StatusCode = response.StatusCode
            };
        }
    }
}