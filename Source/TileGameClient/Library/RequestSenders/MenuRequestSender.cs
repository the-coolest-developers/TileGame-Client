using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TileGameClient.Models;
using TileGameClient.Models.Requests;
using TileGameClient.Models.Responses;
using TileGameClient.Requests.Menu;

namespace TileGameClient.RequestSenders
{
    public class MenuRequestSender : IMenuRequestSender
    {
        private readonly IMenuRequests _menuRequests;
        private readonly IRequestSender _baseRequestSender;

        public MenuRequestSender(IMenuRequests menuRequests)
        {
            _menuRequests = menuRequests;
        }

        public MenuRequestSender(IMenuRequests menu, IRequestSender baseSender)
        {
            _menuRequests = menu;
            _baseRequestSender = baseSender;
        }

        public async Task<string> CreateGame(CreateGameSessionRequest sessionRequest)
        {
            string serialized = JsonConvert.SerializeObject(sessionRequest);
            var content = new StringContent(serialized,
                _baseRequestSender.BaseConfiguration.Encoding,
                _baseRequestSender.BaseConfiguration.Content);

            var res = await _menuRequests.CreateGame(content);

            var sessionId = JsonConvert.DeserializeObject<SessionIdModel>(await res.Content.ReadAsStringAsync());

            return sessionId.SessionId;
        }

        public async Task<ListGameSessionResponse> GetListCreatedGameSessions(int offset, int limit)
        {
            var json = await _menuRequests.GetListCreatedGameSessions(offset, limit);

            var gameSessions = JsonConvert.DeserializeObject<ListGameSessionResponse>(json);

            return gameSessions;
        }

        public async Task JoinGame(SessionIdModel model)
        {
            string serialized = JsonConvert.SerializeObject(model);
            var content = new StringContent(serialized,
                _baseRequestSender.BaseConfiguration.Encoding,
                _baseRequestSender.BaseConfiguration.Content);

            await _menuRequests.JoinGame(content);
        }

        public async Task LeaveGame()
        {
            if (_baseRequestSender.BaseConfiguration.Token != null)
            {
                await _menuRequests.LeaveGame();
            }
        }
    }
}