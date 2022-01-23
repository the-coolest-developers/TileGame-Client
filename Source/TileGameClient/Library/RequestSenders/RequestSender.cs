using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TileGameClient.Models.Configurations;

namespace TileGameClient.RequestSenders
{
    public class RequestSender : IRequestSender
    {
        public HttpClient HttpClient { get; set; }
        public BaseConfiguration BaseConfiguration { get; set; }

        public RequestSender(BaseConfiguration config)
        {
            HttpClient = new HttpClient();
            BaseConfiguration = config;
            HttpClient.Timeout = config.Timeout;

            //НЕ уверен, что нужно так
            HttpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("text/plain"));
        }

        public void SetToken(string token) => HttpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

        public async Task<HttpResponseMessage> Get(string adress)
            => await HttpClient.GetAsync(adress);

        public async Task<HttpResponseMessage> Post(HttpContent content, string adress)
            => await HttpClient.PostAsync(adress, content);
    }
}