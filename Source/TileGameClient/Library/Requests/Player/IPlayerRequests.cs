using System.Net.Http;
using System.Threading.Tasks;

namespace TileGameClient.Requests.Player
{
    public interface IPlayerRequests
    {
        Task<HttpResponseMessage> Register(StringContent content);
        Task<HttpResponseMessage> GetPlayerProfile();
    }
}