using MassTransit;
using SampleApp.Messaging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp.NotificationService
{
    public class UserRegisteredConsumer : IConsumer<IUserRegisteredEvent>
    {
        public async Task Consume(ConsumeContext<IUserRegisteredEvent> context)
        {
            //Send notification to user
            await Console.Out.WriteLineAsync($"User notification sent:  + {context.Message.UserId }");
        }

    }
}
