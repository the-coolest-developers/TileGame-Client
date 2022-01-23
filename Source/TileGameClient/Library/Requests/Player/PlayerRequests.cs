using System.Net.Http;
using System.Threading.Tasks;
using TileGameClient.RequestSenders;

namespace TileGameClient.Requests.Player
{
    public class PlayerRequests : IPlayerRequests
    {
        private readonly IRequestSender _baseSender;

        public PlayerRequests(IRequestSender sender)
        {
            _baseSender = sender;
        }

        public async Task<HttpResponseMessage> Register(StringContent content)
        {
            var res = await _baseSender.Post(content, "https://localhost:44306/players/register");
            return res;
        }

        public async Task<HttpResponseMessage> GetPlayerProfile()
        {
            var response = await _baseSender.Get($"https://localhost:44306/players/profile");

            return response;
        }
    }
}