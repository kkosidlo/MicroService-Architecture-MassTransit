using SampleApp.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.RegistrationService.Messages
{
    public class UserRegisteredEvent : IUserRegisteredEvent
    {
        private IRegisterUserCommand _command;
        private int _userId;

        public UserRegisteredEvent(IRegisterUserCommand command, int userId)
        {
            _command = command;
            _userId = userId;
        }

        public int UserId => _userId;
        public string FirstName => _command.FirstName;
        public string LastName => _command.LastName;
        public string Address => _command.Address;
        public int Age => _command.Age;
        public int IdNumber => _command.IdNumber;
    }
}
