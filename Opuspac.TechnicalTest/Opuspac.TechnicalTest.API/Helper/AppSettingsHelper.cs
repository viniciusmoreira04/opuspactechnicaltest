using Microsoft.Extensions.Configuration;
using System;

public static class AppSettingsHelper
{
    private static IConfigurationRoot _configuration;

    static AppSettingsHelper()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        _configuration = builder.Build();
    }

    public static string GetRabbitMQHostName()
    {
        return _configuration["RabbitMQSettings:HostName"];
    }

    public static int GetRabbitMQPort()
    {
        return (int)Convert.ToInt64(_configuration["RabbitMQSettings:Port"]);
    }

    public static string GetRabbitMQUserName()
    {
        return _configuration["RabbitMQSettings:UserName"];
    }

    public static string GetRabbitMQQueueName()
    {
        return _configuration["RabbitMQSettings:QueueName"];
    }

    public static string GetRabbitMQExchange()
    {
        return _configuration["RabbitMQSettings:Exchange"];
    }

    public static string GetRabbitMQRoutingKey()
    {
        return _configuration["RabbitMQSettings:RoutingKey"];
    }

    public static string GetRabbitMQPassword()
    {
        return _configuration["RabbitMQSettings:Password"];
    }
}
