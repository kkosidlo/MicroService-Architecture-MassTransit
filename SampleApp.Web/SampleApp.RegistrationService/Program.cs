using SampleApp.Messaging;
using System;
using MassTransit;

namespace SampleApp.RegistrationService
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Registration servicwe";

            var bus = BusConfigurator.ConfigureBus((cfg, host) =>
                {
                    cfg.ReceiveEndpoint(host,
                        Constants.RegisterUserServiceQueue, e =>
                        {
                            e.Consumer<RegisterUserCommandConsumer>();
                        });
                });

            bus.Start();

            Console.WriteLine("Listening for register user command.. ");

            Console.ReadLine();

            bus.Stop();
        }
    }
}
