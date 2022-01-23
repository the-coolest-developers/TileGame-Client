using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TileGameClient.Models.Configurations;
using TileGameClient.RequestSenders;

namespace TileGameClient.Requests.Menu
{
    public class MenuRequests : IMenuRequests
    {
        public IRequestSender BaseSender;
        public IBaseConfiguration Configuration;
        public string BaseString;

        public MenuRequests(IRequestSender sender)
        {
            BaseSender = sender;
            Configuration = sender.BaseConfiguration;
            BaseString = Configuration.BaseTgsString + "/menu";
        }

        public async Task<HttpResponseMessage> CreateGame(StringContent content)
        {
            var res = await BaseSender.Post(content,
                                            BaseString + "/createGame");

            return res;
        }

        public async Task<HttpStatusCode> JoinGame(StringContent content)
        {
            var res = await BaseSender.Post(content, $"{BaseString}/joinGame");

            return res.StatusCode;
        }

        public async Task LeaveGame()
        {
            await BaseSender.Get($"{BaseString}/leaveGame");
        }

        public async Task<string> GetListCreatedGameSessions(int offset, int limit)
        {
            string uri = $"{BaseString}/listCreatedGameSessions/{offset}/{limit}/";
            var res = await BaseSender.Get(uri);

            string json = await res.Content.ReadAsStringAsync();
            return json;
        }
    }
}