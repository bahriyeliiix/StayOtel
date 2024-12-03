using HotelService.Infrastructure.Messaging;
using HotelService.Infrastructure.Repositories;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Messaging;
using Serilog;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

public class RabbitMqBackgroundService : BackgroundService
{
    private readonly RabbitMqSettings _rabbitMqSettings;
    private readonly IServiceProvider _serviceProvider;


    public RabbitMqBackgroundService(RabbitMqSettings rabbitMqSettings, IServiceProvider serviceProvider)
    {
        _rabbitMqSettings = rabbitMqSettings;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(_rabbitMqSettings.Host),
                UserName = _rabbitMqSettings.UserName,
                Password = _rabbitMqSettings.Password,
            };

            using var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(_rabbitMqSettings.QueueName, true, false, false);

            Log.Information("Started consuming messages...");

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                await ProcessMessageAsync(message, stoppingToken);
            };
            await channel.BasicConsumeAsync(_rabbitMqSettings.QueueName, true, consumer);

            await Task.Delay(-1, stoppingToken);
        }
        catch (Exception ex)
        {
            Log.Error($"Error occurred: {ex.Message}");
        }
    }

    private async Task ProcessMessageAsync(string message, CancellationToken cancellationToken)
    {
        try
        {
            Log.Information("Processing message: {Message}", message);

            var reportMessage = JsonSerializer.Deserialize<CreateReportMessage>(message);

            if (reportMessage == null)
            {
                Log.Warning("Failed to deserialize message: {Message}", message);
                return;
            }


            using var scope = _serviceProvider.CreateScope();
            var repositoryFactory = scope.ServiceProvider.GetRequiredService<IHotelRepositoryFactory>();

            var repository = repositoryFactory.Create();
            var result = await repository.GenerateReport(reportMessage.Location);
            if (result != null)
            {
                result.ReportId = reportMessage.ReportId;

                var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(result));

                await PublishReportResponseAsync(body);
            }
            else
            {
                Log.Warning("Report generation failed for location: {Location}", reportMessage.Location);
            }
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while processing the message.");
        }
    }

    private async Task PublishReportResponseAsync(byte[] body)
    {
        try
        {
            var factory = new ConnectionFactory
            {
                Uri = new Uri(_rabbitMqSettings.Host),
            };

            using var connection = await factory.CreateConnectionAsync();
            var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(queue: _rabbitMqSettings.QueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);

            await channel.BasicPublishAsync(exchange: string.Empty, routingKey: _rabbitMqSettings.ReQueueName, body: body);

            Log.Information("Report response published to queue: {QueueName}", _rabbitMqSettings.ReQueueName);

            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while publishing the report response.");
        }
    }
}
