namespace MessageTests
{
    using System;
    using System.Reflection;
    using Messages.V1;
    using Shouldly;
    using Xunit;

    public class NewUserMessageTests
    {
        private const string BrokenContractForV1 = "Broken contract for V1:";

        //Follow this guideline: https://docs.particular.net/nservicebus/messaging/evolving-contracts
        [Fact]
        public void TestNewUserMessageV1Contract()
        {
            var newUserType = typeof(NewUser);

            AssertVersionInfo(newUserType);

            AssertPropertySpecs(newUserType);

            AssertParameterLessConstructor(newUserType);
        }

        private static void AssertParameterLessConstructor(Type newUserType)
        {
            var constructors = newUserType.GetConstructors(BindingFlags.Default);

            constructors.ShouldNotBeNull(customMessage:$"{BrokenContractForV1} default constructor is not found.");
        }

        private static void AssertVersionInfo(Type newUserType)
        {
            (newUserType.Namespace != null && newUserType.Namespace.Contains("V1")).ShouldBe(true, customMessage: $"{BrokenContractForV1} version number is wrong.");
        }

        private static void AssertPropertySpecs(Type newUserType)
        {
            var properties = newUserType.GetProperties();

            var expected = 5;

            properties.Length.ShouldBe(expected, customMessage: $"{BrokenContractForV1} no of properties should be {expected}");

            AssertPropertySpecs<Guid?>(properties[0], "Id");

            AssertPropertySpecs<string>(properties[1], "FirstName");

            AssertPropertySpecs<string>(properties[2], "LastName");

            AssertPropertySpecs<DateTime?>(properties[3], "CreatedOn");

            AssertPropertySpecs<DateTime?>(properties[4], "ModifiedOn");

        }

        private static void AssertPropertySpecs<TExpected>(PropertyInfo propertyUnderTest, string expectedName)
        {
            propertyUnderTest.ShouldNotBeNull(customMessage:$"{BrokenContractForV1} {expectedName} not found.");

            var expectedType = typeof(TExpected);

            propertyUnderTest.PropertyType.ShouldBe(expectedType, customMessage: $"{BrokenContractForV1}  {expectedName} is not of the type {expectedType.Name}.");

            propertyUnderTest.PropertyType.IsPublic.ShouldBeTrue(customMessage: $"{BrokenContractForV1}  {expectedName} is not public.");

            propertyUnderTest.Name.ShouldBe(expectedName, customMessage: $"{BrokenContractForV1}  {expectedName} not found.");

            propertyUnderTest.SetMethod.IsPublic.ShouldBe(true); // Don't make class immutable..There should be public setters
        }
    }
}
