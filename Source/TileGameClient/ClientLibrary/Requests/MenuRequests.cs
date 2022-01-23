using ClientLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientLibrary.Requests
{
    public class MenuRequests
    {
        public BaseRequestSender _baseSender;

        public MenuRequests() 
        {
            _baseSender = new BaseRequestSender(TimeSpan.FromSeconds(30));//Сделать по-другому
        }

        public async Task<string> CreateGame(CreateGameModel model) 
        {
            var res = await _baseSender.Post(new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json-patch+json"),
                "https://localhost:44306/menu/createGame");
            var sessionId = JsonConvert.DeserializeObject<SessionIdModel>(await res.Content.ReadAsStringAsync());

            return sessionId.sessionId;
        
            //Тут нужно иначе задавать тип, как сейчас в примере
        }

        public async Task<string> JoinGame(SessionIdModel model) 
        { string smth = JsonConvert.SerializeObject(model);
            var content = new StringContent(smth, Encoding.UTF8, "application/json-patch+json");
            var res = await _baseSender.Post(content,
                "https://localhost:44306/menu/joinGame");

            var token = JsonConvert.DeserializeObject<Token>(await res.Content.ReadAsStringAsync());
            return token.token;
        }

        public async Task LeaveGame() 
        {
            var res = await _baseSender.Post(new StringContent(JsonConvert.SerializeObject(new object()), Encoding.UTF8, "application/json-patch+json"),
                "https://localhost:44306/menu/leaveGame");
        }

        public async Task<GameSession[]> GetListCreatedGameSessions(int offset, int limit) 
        {
            string uri = "https://localhost:44306/menu/listCreatedGameSessions/" + offset +"/" + limit + "/";
            var res = await _baseSender.Get(uri);

            string json = await res.Content.ReadAsStringAsync();

            List<GameSession> gameSessions = JsonConvert.DeserializeObject<Root>(json).gameSessions;
            return gameSessions.ToArray();
        }

        public void Authorize(string token) => _baseSender.SetToken(token);
    }
}
