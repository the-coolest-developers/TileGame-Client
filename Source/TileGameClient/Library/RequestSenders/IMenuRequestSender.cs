using System.Threading.Tasks;
using TileGameClient.Models;
using TileGameClient.Models.Requests;
using TileGameClient.Models.Responses;

namespace TileGameClient.RequestSenders
{
    public interface IMenuRequestSender
    {
        Task<string> CreateGame(CreateGameSessionRequest sessionRequest);

        Task JoinGame(SessionIdModel model);

        Task LeaveGame();

        Task<ListGameSessionResponse> GetListCreatedGameSessions(int offset, int limit);
    }
}