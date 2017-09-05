namespace SyncUsersEndpoints
{
    using System;
    using System.Text;
    using System.Threading.Tasks;
    using Messages;
    using Newtonsoft.Json.Linq;
    using NServiceBus;
    using RabbitMQ.Client;

    public class SqlServerMessageHandler : IHandleMessages<NewUser>
    {
        public async Task Handle(NewUser message, IMessageHandlerContext context)
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };
            
            await Task.Run(() =>
            {
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        var properties = channel.CreateBasicProperties();
                        properties.MessageId = Guid.NewGuid().ToString();

                        var jObjectFromMessage = JObject.FromObject(message);
                        var typeName = typeof(NewUser).FullName;
                        jObjectFromMessage.AddFirst(new JProperty("$type", typeName));
                        var serializedMessage = jObjectFromMessage.ToString();
                        var messageBytes = Encoding.UTF8.GetBytes(serializedMessage);
                        channel.QueueDeclare("SyncUsers.RabbitMqEndpoint", true, autoDelete: false,
                            exclusive: false);
                        channel.BasicPublish(string.Empty, "SyncUsers.RabbitMqEndpoint", false, properties,
                            messageBytes);
                    }
                }
            });
        }
    }
}

