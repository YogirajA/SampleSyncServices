namespace MessageTests
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Messages.V1;
    using Shouldly;
    using Xunit;

    public class NewUserMessageTests
    {
        //https://docs.particular.net/nservicebus/messaging/evolving-contracts
        [Fact]
        public void TestNewUserMessageV1Contract()
        {
            var newUserType = typeof(NewUser);

            newUserType.Namespace.Contains("V1").ShouldBe(true);

            var properties = newUserType.GetProperties();

            properties.Length.ShouldBe(5);

            AssertPropertySpecs(properties.FirstOrDefault(t => t.Name.Equals(nameof(NewUser.Id))), typeof(Guid?));

            AssertPropertySpecs(properties.FirstOrDefault(t => t.Name.Equals(nameof(NewUser.FirstName))),
                typeof(string));

            AssertPropertySpecs(properties.FirstOrDefault(t => t.Name.Equals(nameof(NewUser.LastName))),
                typeof(string));

            AssertPropertySpecs(properties.FirstOrDefault(t => t.Name.Equals(nameof(NewUser.CreatedOn))),
                typeof(DateTime?));
            AssertPropertySpecs(properties.FirstOrDefault(t => t.Name.Equals(nameof(NewUser.ModifiedOn))),
                typeof(DateTime?));
        }

        private static void AssertPropertySpecs(PropertyInfo firstOrDefault, Type propertyType)
        {
            firstOrDefault.ShouldNotBeNull();

            firstOrDefault.PropertyType.ShouldBe(propertyType);

            firstOrDefault.PropertyType.IsPublic.ShouldBeTrue();
        }
    }
}
