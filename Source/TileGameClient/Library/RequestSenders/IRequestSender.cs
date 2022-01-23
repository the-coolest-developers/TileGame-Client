using System.Net.Http;
using System.Threading.Tasks;
using TileGameClient.Models.Configurations;

namespace TileGameClient.RequestSenders
{
    public interface IRequestSender
    {
        HttpClient HttpClient { get; set; }
        BaseConfiguration BaseConfiguration { get; set; }
        void SetToken(string token);
        Task<HttpResponseMessage> Get(string adress);

        Task<HttpResponseMessage> Post(HttpContent content, string adress);
    }
}
