using ClientLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Requests
{
    public class SSORequests // Разбросать класс на отдельные чтобы ничего не нарушать
    {
        string _content_type;
        Encoding _encoding;
        public BaseRequestSender _baseSender;
        
        public SSORequests(TimeSpan timeout, string content, Encoding encoding) 
        {
            _baseSender = new BaseRequestSender(timeout);
            _content_type = content;
            _encoding = encoding;
        }
        
        public async Task<string> LogIn(LoginModel model)
        {
            var res = await _baseSender.Post(new StringContent(JsonConvert.SerializeObject(model), _encoding, _content_type),
                                            "https://localhost:44317/login");

            string json = await res.Content.ReadAsStringAsync();
            Token accountsToken = JsonConvert.DeserializeObject<Token>(json);

            return accountsToken.token;
        }

        public async Task<HttpStatusCode> Register(RegisterModel model) 
        {
            var res = await _baseSender.Post(new StringContent(JsonConvert.SerializeObject(model), _encoding, _content_type),
                                             "https://localhost:44317/register");

            return res.StatusCode;
        }

        public async Task<Account> GetAccount() 
        {
            var res = await _baseSender.Get("https://localhost:44317/get");

            string json = await res.Content.ReadAsStringAsync();
            Account account = JsonConvert.DeserializeObject<Account>(json);

            return account;
        }

        public void Authorize(string token) => _baseSender.SetToken(token);
    }
}
