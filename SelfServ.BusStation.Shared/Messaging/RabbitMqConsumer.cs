using MediatR;
using System.Text;
using RabbitMQ.Client;
using System.Text.Json;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.Hosting;
using SelfServ.BusStation.Shared.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SelfServ.BusStation.Shared.Messaging
{
    public class RabbitMqConsumer : BackgroundService
    {
        private IChannel? _channel;
        private IConnection? _connection;
        private readonly string _hostName;
        private readonly string _queueName;
        private readonly string _routingKey;
        private readonly string _exchangeName;
        private readonly IServiceScopeFactory _scopeFactory;
        public RabbitMqConsumer(IConfiguration configuration,
                                IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
            _hostName = configuration["RabbitMQ:Host"];
            _queueName = configuration["RabbitMQ:QueueName"];
            _exchangeName = configuration["RabbitMQ:Exchange"];
            _exchangeName = configuration["RabbitMQ:RoutingKey"];
        }
        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            var factory = new ConnectionFactory { HostName = _hostName };

            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.ExchangeDeclareAsync(exchange: _exchangeName, 
                                                type: ExchangeType.Direct, 
                                                durable: true);

            await _channel.QueueDeclareAsync(queue: _queueName,
                                             durable: true,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);

            await _channel.QueueBindAsync(queue: _queueName, 
                                          exchange: _exchangeName, 
                                          routingKey: _routingKey);

            await base.StartAsync(cancellationToken);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                var baseMessage = JsonSerializer.Deserialize<BaseMessage<JsonElement>>(message);

                Type dtoType = AppDomain.CurrentDomain
                                        .GetAssemblies()
                                        .SelectMany(a => a.GetTypes())
                                        .FirstOrDefault(t => t.Name == baseMessage.MessageType);

                if (dtoType == null)
                    throw new NullReferenceException($"Type {baseMessage.MessageType} not found");

                var payload = JsonSerializer.Deserialize(baseMessage.Payload.ToString(), dtoType);

                using (var scope = _scopeFactory.CreateScope())
                {
                    Type commandType = AppDomain.CurrentDomain
                                                .GetAssemblies()
                                                .SelectMany(a => a.GetTypes())
                                                .FirstOrDefault(t => t.Name == baseMessage.HandlerType);

                    if (commandType == null)
                        throw new NullReferenceException($"Type {baseMessage.HandlerType} not found");

                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    var command = Activator.CreateInstance(commandType, payload);

                    if (typeof(IRequest).IsAssignableFrom(commandType))
                    {
                        await (Task)mediator.GetType().GetMethod("Send")
                                                      .Invoke(mediator, new object[] { command, stoppingToken });
                    }
                    else if (typeof(INotification).IsAssignableFrom(commandType))
                    {
                        await (Task)mediator.GetType().GetMethod("Publish")
                                                      .Invoke(mediator, new object[] { command, stoppingToken });
                    }
                }
            };

            await _channel.BasicConsumeAsync(queue: _queueName, autoAck: true, consumer: consumer);
        }
        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _channel?.CloseAsync();
            await _connection?.CloseAsync();
            await base.StopAsync(cancellationToken);
        }
    }
}
