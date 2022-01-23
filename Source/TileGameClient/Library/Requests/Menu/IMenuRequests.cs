using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TileGameClient.Requests.Menu
{
    public interface IMenuRequests
    {
        Task<HttpResponseMessage> CreateGame(StringContent content);

        Task<HttpStatusCode> JoinGame(StringContent content);

        Task LeaveGame();

        Task<string> GetListCreatedGameSessions(int offset, int limit);
    }
}
