namespace ReportService.Infrastructure.Messaging
{
    public class RabbitMqSettings
    {
        public string Host { get; set; } 
        public string QueueName { get; set; } 
        public string ReQueueName { get; set; } 
    }
}
