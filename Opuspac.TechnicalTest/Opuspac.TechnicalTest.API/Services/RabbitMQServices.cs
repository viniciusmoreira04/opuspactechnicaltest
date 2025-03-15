using Newtonsoft.Json;
using Opuspac.TechnicalTest.Application.DTOs;
using Opuspac.TechnicalTest.Domain;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Opuspac.TechnicalTest.API.Services
{
    public class RabbitMQServices
    {
        public static async void PublishRabbitMQ(OrderService orderSevice)
        {
            var factory = new ConnectionFactory()
            {
                HostName = AppSettingsHelper.GetRabbitMQHostName(),
                Port = AppSettingsHelper.GetRabbitMQPort(),
                UserName = AppSettingsHelper.GetRabbitMQUserName(),
                Password = AppSettingsHelper.GetRabbitMQPassword(),
                VirtualHost = "/"
            };

            using (var connection = await factory.CreateConnectionAsync())
            using (var channel = await connection.CreateChannelAsync())
            {
                await channel.QueueDeclareAsync(queue: AppSettingsHelper.GetRabbitMQQueueName(),
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = JsonConvert.SerializeObject(orderSevice);
                var body = Encoding.UTF8.GetBytes(message);

                await channel.BasicPublishAsync(
                exchange: AppSettingsHelper.GetRabbitMQExchange(),
                routingKey: AppSettingsHelper.GetRabbitMQRoutingKey(),
                body: new ReadOnlyMemory<byte>(body)
            );
            }
        }

        public static async Task<OrderServiceDTO> ConsumerRabbitMQ()
        {
            OrderServiceDTO orderService = null;

            var factory = new ConnectionFactory()
            {
                HostName = AppSettingsHelper.GetRabbitMQHostName(),
                Port = AppSettingsHelper.GetRabbitMQPort(),
                UserName = AppSettingsHelper.GetRabbitMQUserName(),
                Password = AppSettingsHelper.GetRabbitMQPassword(),
                VirtualHost = "/"
            };

            using (var connection = await factory.CreateConnectionAsync())
            using (var channel = await connection.CreateChannelAsync())
            {
                await channel.QueueDeclareAsync(queue: AppSettingsHelper.GetRabbitMQQueueName(),
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                arguments: null);

                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    orderService = JsonConvert.DeserializeObject<OrderServiceDTO>(message);
                    return Task.CompletedTask;
                };

                await channel.BasicConsumeAsync(queue: AppSettingsHelper.GetRabbitMQQueueName(),
                                     autoAck: false,
                                     consumer: consumer);
            }

            return orderService;
        }
    }
}
