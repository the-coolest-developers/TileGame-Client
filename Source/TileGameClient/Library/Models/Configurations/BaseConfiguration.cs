using System;
using System.Text;

namespace TileGameClient.Models.Configurations
{
    public class BaseConfiguration : IBaseConfiguration
    {
        public TimeSpan Timeout { get; set; }
        public string BaseSsoString { get; set; }
        public string BaseTgsString { get; set; }
        public Encoding Encoding { get; set; }
        public string Content { get; set; }
        public string Token { get; set; }
    }
}
