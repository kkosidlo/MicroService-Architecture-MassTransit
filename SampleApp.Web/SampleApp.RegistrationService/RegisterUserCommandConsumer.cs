using MassTransit;
using SampleApp.Messaging;
using SampleApp.RegistrationService.Messages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.RegistrationService
{
    public class RegisterUserCommandConsumer : IConsumer<IRegisterUserCommand>
    {
        public async Task Consume(ConsumeContext<IRegisterUserCommand> context)
        {
            var command = context.Message;

            var id = 12;

            await Console.Out.WriteLineAsync($"User with id {id} registered");
            
            //notify subscribers that user is registered.

            var userRegisteredEvent = new UserRegisteredEvent(command, id);

            await context.Publish<IRegisterUserCommand>(userRegisteredEvent);
        }
    } 
}
