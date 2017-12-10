using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.Messaging
{
    public interface IRegisterUserCommand
    {
        string FirstName { get; }
        string LastName { get; }
        string Address { get; }
        int Age { get; }
        int IdNumber { get; }
    }
}