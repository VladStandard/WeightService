// https://stackexchange.github.io/StackExchange.Redis/Basics
namespace Ws.Domain.Services.Redis;

internal class RedisSettingsModel
{
    public string Server { get; set; } = string.Empty;
    public short Port { get; set; }

    public string GetConnectionString() => $"{Server}:{Port}";
}