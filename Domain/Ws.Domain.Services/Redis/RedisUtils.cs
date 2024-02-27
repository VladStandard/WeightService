using Microsoft.Extensions.Configuration;

namespace Ws.Domain.Services.Redis;

public static class RedisUtils
{
    internal static IConfigurationRoot LoadRedisCfg()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("redis_config.json", optional: false, reloadOnChange: false)
            .Build();
    }
}