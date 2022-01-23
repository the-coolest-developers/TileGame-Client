using System.Net;
using System.Threading.Tasks;
using TileGameClient.Infrastructure.Responses;
using TileGameClient.Models;
using TileGameClient.Models.Requests;
using TileGameClient.Models.Responses;

namespace TileGameClient.RequestSenders
{
    public interface IPlayerRequestSender
    {
        Task<HttpStatusCode> RegisterPlayer(RegisterPlayerRequest request);
        Task<HttpStatusCodeResponse<GetPlayerProfileResponse>> GetPlayerProfile();
    }
}