using System.Text;
using RabbitMQ.Client;
using System.Text.Json;
using SelfServ.BusStation.Shared.Models;
using Microsoft.Extensions.Configuration;

namespace SelfServ.BusStation.Shared.Messaging
{
    public class RabbitMqPublisher
    {
        private readonly string _hostName;
        private readonly string _exchangeName;

        public RabbitMqPublisher(IConfiguration configuration)
        {
            _hostName = configuration["RabbitMQ:Host"];
        }
        public async Task Publish<T>(T message,string exchangeName, string routingKey)
        {
            var factory = new ConnectionFactory { HostName = _hostName };

            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(exchange: _exchangeName, 
                                               type: ExchangeType.Direct, 
                                               durable: true);

            var baseMessage = new BaseMessage<T>(message, typeof(T).Name);
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(baseMessage));

            await channel.BasicPublishAsync(exchange: _exchangeName,
                                            routingKey: routingKey, 
                                            body: body);
        }
    }
}
