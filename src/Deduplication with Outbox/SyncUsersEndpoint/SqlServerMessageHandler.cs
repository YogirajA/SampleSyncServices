namespace SyncUsersEndpoints
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using Messages.V1;
    using Newtonsoft.Json.Linq;
    using NServiceBus;
    using RabbitMQ.Client;

    public class SqlServerMessageHandler : IHandleMessages<NewUser>
    {
        private readonly IModel _model;

        public SqlServerMessageHandler(IModel model)
        {
            _model = model;
        }

        public async Task Handle(NewUser message, IMessageHandlerContext context)
        {
            await Task.Run(() =>
            {
                var typeName = typeof(NewUser).FullName;
                var properties = _model.CreateBasicProperties();
                properties.MessageId = Guid.NewGuid().ToString();
                properties.Headers =
                    new Dictionary<string, object> {{"NServiceBus.EnclosedMessageTypes", typeName}};

                var jObjectFromMessage = JObject.FromObject(message);
                //  jObjectFromMessage.AddFirst(new JProperty("$type", typeName));
                var serializedMessage = jObjectFromMessage.ToString();
                var messageBytes = Encoding.UTF8.GetBytes(serializedMessage);
                _model.QueueDeclare("SyncUsers.RabbitMqEndpoint", true, autoDelete: false,
                    exclusive: false);
                _model.BasicPublish(string.Empty, "SyncUsers.RabbitMqEndpoint", false, properties,
                    messageBytes);
            });
        }
    }
}

