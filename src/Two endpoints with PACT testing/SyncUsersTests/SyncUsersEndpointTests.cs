namespace SyncUsersTests
{
    using System.Threading.Tasks;
    using Messages;
    using NServiceBus.Testing;
    using RabbitMQ.Fakes;
    using Shouldly;
    using SyncUsersEndpoints;
    using Xunit;

    public class SyncUsersEndpointTests
    {
        [Fact]
        public void HandlesMessageSuccessfully()
        {
            var context = new TestableMessageHandlerContext();
            
            var fakeModel = new FakeModel(new RabbitServer());

            var handler = new SqlServerMessageHandler(fakeModel);

            Should.NotThrow(async () => await handler.Handle(new NewUser(), context).ConfigureAwait(false));

        }
    }
}
