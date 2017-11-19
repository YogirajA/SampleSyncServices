namespace Messages.V1
{
    using System;
    using NServiceBus;

    //It is recommended to have Nullable properties in the messages than defaults such as DateTime.MinValue or Int defaulting to 0
    //It is also recommended to have Version in the namespace
    public class NewUser:IMessage
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
