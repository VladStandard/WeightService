using Microsoft.Extensions.Configuration;

namespace Ws.Domain.Services.Redis;

public static class RedisUtils
{
    internal static RedisSettingsModel LoadJsonConfig()
    {
        IConfigurationRoot sqlConfiguration = new ConfigurationBuilder()
            .AddJsonFile("redis_config.json", optional: false, reloadOnChange: false)
            .Build();

        RedisSettingsModel redisSettingsModels = new();
        sqlConfiguration.GetSection("RedisSettings").Bind(redisSettingsModels);
        return redisSettingsModels;
    }
}