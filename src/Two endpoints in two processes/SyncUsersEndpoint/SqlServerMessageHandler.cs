namespace SyncUsersEndpoints
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Messages;
    using NServiceBus;
    using RabbitMQ.Client;
    using Newtonsoft.Json.Linq;

    public class SqlServerMessageHandler : IHandleMessages<NewUser>
    {
        public Task Handle(NewUser message, IMessageHandlerContext context)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"

            };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {

                    var properties = channel.CreateBasicProperties();
                    properties.MessageId = Guid.NewGuid().ToString();

                    var jObjectFromMessage = JObject.FromObject(message);
                    jObjectFromMessage.AddFirst(new JProperty("$type", $"{nameof(Messages)}.{nameof(NewUser)}"));
                    var json = jObjectFromMessage.ToString();
                    var rabbitMessage = Encoding.UTF8.GetBytes(json);

                    channel.QueueDeclare("SyncUsers.RabbitMqEndpoint",durable:true, autoDelete:false,exclusive: false);
                    channel.BasicPublish(string.Empty, "SyncUsers.RabbitMqEndpoint", false, properties, rabbitMessage);

                }
                
            }
            return Task.CompletedTask;
        }
    }

}