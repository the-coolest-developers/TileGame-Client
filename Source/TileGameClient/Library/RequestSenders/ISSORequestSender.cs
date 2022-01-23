using System.Net;
using System.Threading.Tasks;
using TileGameClient.Models;
using TileGameClient.Models.Requests;

namespace TileGameClient.RequestSenders
{
    public interface ISsoRequestSender
    {
        void Authorize(string token);

        Task<string> LogIn(LoginRequest request);

        Task<HttpStatusCode> Register(RegisterAccountRequest accountRequest);

        Task<Account> GetAccount();
    }
}
