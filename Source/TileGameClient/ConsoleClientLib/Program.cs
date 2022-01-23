using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TileGameClient.Models;
using TileGameClient.Models.Configurations;
using TileGameClient.Models.Requests;
using TileGameClient.Requests.Menu;
using TileGameClient.Requests.Player;
using TileGameClient.Requests.SSO;
using TileGameClient.RequestSenders;

namespace ConsoleClient
{
    class Program
    {
        //public static RequestSender sender;

        static async Task Main(string[] args)
        {
            BaseConfiguration configuration = new BaseConfiguration();

            configuration.BaseSsoString = "https://localhost:44317";
            configuration.BaseTgsString = "https://localhost:44306";
            //configuration.baseTGSString = "18.192.198.228:"

            configuration.Timeout = TimeSpan.FromSeconds(30);
            configuration.Content = "application/json-patch+json";
            configuration.Encoding = Encoding.UTF8;

            RequestSender sender = new RequestSender(configuration);

            MenuRequestSender menuRequestSender = new MenuRequestSender(
                new MenuRequests(sender), sender);
            SsoRequestSender ssoRequestSender = new SsoRequestSender(
                new SsoRequests(sender), sender);

            PlayerRequestSender playerRequestSender = new PlayerRequestSender(
                new PlayerRequests(sender), sender);

            /*var res = await requestSender.Register(new RegisterModel
            {
                email = "12345s6@gmail.com",
                firstName = "UAsdddfLфываERIC",
                lastName = "aasddsdsd",
                password = "string"
            });*/

            var accToken = await ssoRequestSender.LogIn(new LoginRequest
            {
                Email = "12345s6@gmail.com",
                Password = "string"
            });

            ssoRequestSender.Authorize(accToken);

            var playerProfile = await playerRequestSender.GetPlayerProfile();
            if (playerProfile.StatusCode == HttpStatusCode.Conflict)
            {
                Console.WriteLine("Player does not exist. Registering...");

                string nickname = "Nicknammmmmeee";

                var r = await playerRequestSender.RegisterPlayer(new RegisterPlayerRequest()
                {
                    Nickname = nickname
                });

                if (r == HttpStatusCode.OK)
                {
                    Console.WriteLine("Player was registered successfully");
                }
            }

            var sessionId = await menuRequestSender.CreateGame(
                new CreateGameSessionRequest
                {
                    SessionCapacity = 5
                });


            await menuRequestSender.JoinGame(
                new SessionIdModel
                {
                    SessionId = sessionId
                });
            await menuRequestSender.LeaveGame();

            var games = await menuRequestSender.GetListCreatedGameSessions(10, 5);
        }
    }
}