using System;
using System.Text;

namespace TileGameClient.Models.Configurations
{
    public interface IBaseConfiguration
    {
        TimeSpan Timeout { get; set; }
        string BaseSsoString { get; set; }
        string BaseTgsString { get; set; }
        Encoding Encoding { get; set; }
        string Content { get; set; }
        string Token { get; set; }
    }
}
