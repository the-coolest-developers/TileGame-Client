using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using TileGameClient.Infrastructure.Responses;
using TileGameClient.Models;
using TileGameClient.Models.Responses;

namespace TileGameClient.Requests.SSO
{
    public interface ISsoRequests
    {
        Task<HttpStatusCodeResponse<LoginResponse>> LogIn(StringContent content);

        Task<HttpStatusCode> Register(StringContent content);

        Task<Account> GetAccount();
    }
}