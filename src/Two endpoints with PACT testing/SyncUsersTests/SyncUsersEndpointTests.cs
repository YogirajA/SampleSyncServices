namespace SyncUsersTests
{
	using System;
	using System.Linq;
	using System.Reflection;
	using System.Threading.Tasks;
    using Messages;
	using Messages.V1;
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
		//https://docs.particular.net/nservicebus/messaging/evolving-contracts
		[Fact]
        public void TestServicePact()
        {
	        var ob = typeof(NewUser);

			var properties = ob.GetProperties();

	        properties.Length.ShouldBe(5);

	        AssertPropertySpecs(properties.FirstOrDefault(t => t.Name.Equals(nameof(NewUser.Id))), typeof(Guid));

			AssertPropertySpecs(properties.FirstOrDefault(t => t.Name.Equals(nameof(NewUser.FirstName))), typeof(string));
	        
			AssertPropertySpecs(properties.FirstOrDefault(t => t.Name.Equals(nameof(NewUser.LastName))), typeof(string));

            AssertPropertySpecs(properties.FirstOrDefault(t => t.Name.Equals(nameof(NewUser.CreatedOn))), typeof(DateTime));
	        AssertPropertySpecs(properties.FirstOrDefault(t => t.Name.Equals(nameof(NewUser.ModifiedOn))), typeof(DateTime));
        }

	    private static void AssertPropertySpecs(PropertyInfo firstOrDefault, Type propertyType)
	    {
		    firstOrDefault.ShouldNotBeNull();

		    firstOrDefault.PropertyType.ShouldBe(propertyType);

		    firstOrDefault.PropertyType.IsPublic.ShouldBeTrue();
	    }
    }
}
