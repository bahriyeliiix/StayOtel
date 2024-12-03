using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Shared.Messaging;
using Serilog;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using ReportService.Infrastructure.Messaging;
using ReportService.Infrastructure.Factory;

public class RabbitMqReportBackgroundService : BackgroundService
{
    private readonly RabbitMqSettings _rabbitMqSettings;
    private readonly IServiceProvider _serviceProvider;

    public RabbitMqReportBackgroundService(RabbitMqSettings rabbitMqSettings, IServiceProvider serviceProvider)
    {
        _rabbitMqSettings = rabbitMqSettings;
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Log.Information("RabbitMQ Background Service is starting...");
        try
        {
            var factory = new ConnectionFactory()
            {
                Uri = new Uri(_rabbitMqSettings.Host),
            };

            using var connection = await factory.CreateConnectionAsync();
            Log.Information("Connected to RabbitMQ at {Host}", _rabbitMqSettings.Host);

            var channel = await connection.CreateChannelAsync();
            Log.Information("Channel created successfully.");

            await channel.QueueDeclareAsync(_rabbitMqSettings.ReQueueName, true, false, false);
            Log.Information("Queue declared: {QueueName}", _rabbitMqSettings.ReQueueName);

            Log.Information("Started consuming messages from queue: {QueueName}", _rabbitMqSettings.ReQueueName);

            var consumer = new AsyncEventingBasicConsumer(channel);

            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);

                Log.Information("Received message: {Message}", message);
                await ProcessMessageAsync(message, stoppingToken);
            };

            await channel.BasicConsumeAsync(_rabbitMqSettings.ReQueueName, true, consumer);
            Log.Information("Consumer registered and waiting for messages...");

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred while setting up the RabbitMQ Background Service.");
        }
    }

    private async Task ProcessMessageAsync(string message, CancellationToken cancellationToken)
    {
        Log.Information("Processing message: {Message}", message);
        try
        {
            var reportMessage = JsonSerializer.Deserialize<ResponseReportMessage>(message);

            if (reportMessage == null)
            {
                Log.Warning("Failed to deserialize the message. Message content: {Message}", message);
                return;
            }

            Log.Information("Deserialized message successfully. ReportId: {ReportId}, Location: {Location}", reportMessage.ReportId, reportMessage.Location);

            var reportData = new ReportData
            {
                Id = reportMessage.ReportId,
                HotelCount = reportMessage.HotelCount,
                Location = reportMessage.Location,
                PhoneCount = reportMessage.PhoneCount,
            };

            using var scope = _serviceProvider.CreateScope();
            var repositoryFactory = scope.ServiceProvider.GetRequiredService<IReportRepositoryFactory>();

            var repository = repositoryFactory.Create();
            Log.Information("Repository factory created successfully.");

            var result = await repository.UpdateAsync(reportData);
            if (result != null)
            {
                Log.Information("Report updated successfully. ReportId: {ReportId}", reportMessage.ReportId);
            }
            else
            {
                Log.Warning("Failed to update report for location: {Location}. ReportId: {ReportId}", reportMessage.Location, reportMessage.ReportId);
            }
        }
        catch (JsonException jsonEx)
        {
            Log.Error(jsonEx, "Failed to deserialize the message: {Message}", message);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An unexpected error occurred while processing the message: {Message}", message);
        }
    }
}
