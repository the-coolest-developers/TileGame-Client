namespace TileGameClient.Models
{
    public class GameSession
    {
        public string Id { get; set; }
        public string CreatorNickname { get; set; }
        public int Capacity { get; set; }
        public int PlayerAmount { get; set; }
    }
}