using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using ReportService.Application.Interfaces;
using Shared.Messaging;

namespace ReportService.Infrastructure.Messaging
{
    public class ReportProducer : IReportProducer
    {
        private readonly RabbitMqSettings _rabbitMqSettings;

        public ReportProducer(IOptions<RabbitMqSettings> rabbitMqSettings)
        {
            _rabbitMqSettings = rabbitMqSettings.Value;
        }

        public async Task PublishReportRequestAsync(CreateReportMessage message)
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(_rabbitMqSettings.Host),
            };

            using var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            channel.QueueDeclareAsync(queue: _rabbitMqSettings.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: _rabbitMqSettings.QueueName, body: body);

            await Task.CompletedTask;
        }
    }
}
