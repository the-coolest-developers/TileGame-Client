using System.Collections.Generic;

namespace ClientLibrary.Models
{
    public class GameSession
    {
        public string id { get; set; }
        public string creatorNickname { get; set; }
        public int capacity { get; set; }
        public int playerAmount { get; set; }
    }
    public class Root
    {
        public List<GameSession> gameSessions { get; set; }
    }
}
