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

            AssertPropertySpecs<Guid?>(properties[0],"Id");

            AssertPropertySpecs<string>(properties[1], "FirstName");

            AssertPropertySpecs<string>(properties[2], "LastName");

            AssertPropertySpecs<DateTime?>(properties[3], "CreatedOn");

            AssertPropertySpecs<DateTime?>(properties[4], "ModifiedOn");
        }

        private static void AssertPropertySpecs<TExpected>(PropertyInfo propertyUnderTest,string expectedName)
        {
            propertyUnderTest.ShouldNotBeNull();

            propertyUnderTest.PropertyType.ShouldBe(typeof(TExpected));

            propertyUnderTest.PropertyType.IsPublic.ShouldBeTrue();

            propertyUnderTest.Name.ShouldBe(expectedName);
        
        }
    }
}
