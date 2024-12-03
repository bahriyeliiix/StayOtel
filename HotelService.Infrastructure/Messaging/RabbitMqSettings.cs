namespace HotelService.Infrastructure.Messaging
{
    public class RabbitMqSettings
    {
        public string Host { get; set; }
        public string QueueName { get; set; }
        public string ReQueueName { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string VirtualHost { get; set; }
    }
}
