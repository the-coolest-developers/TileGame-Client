using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Requests
{
    public class BaseRequestSender
    {
        public HttpClient httpClient { get; set; }

        public BaseRequestSender(TimeSpan timeout)
        {
            httpClient = new HttpClient();
            httpClient.Timeout = timeout;
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("text/plain"));
        }

        public void SetToken(string token) => httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
        //public void SetContentType(string type) => httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");

        public async Task<HttpResponseMessage> Get(string adress)
            => await httpClient.GetAsync(adress);

        public async Task<HttpResponseMessage> Post(HttpContent content, string adress)
            => await httpClient.PostAsync(adress, content);
    }
}
