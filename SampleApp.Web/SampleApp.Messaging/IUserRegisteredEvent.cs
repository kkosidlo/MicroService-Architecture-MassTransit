using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.Messaging
{
    public interface IUserRegisteredEvent
    {
        int UserId { get; }
        string FirstName { get; }
        string LastName { get; }
        string Address { get; }
        int Age { get; }
        int IdNumber { get; }
    }
}
