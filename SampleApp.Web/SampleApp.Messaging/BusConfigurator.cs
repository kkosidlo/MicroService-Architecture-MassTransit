using MassTransit;
using MassTransit.RabbitMqTransport;
using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.Messaging
{
    public static class BusConfigurator
    {
        public static IBusControl ConfigureBus(
            Action<IRabbitMqBusFactoryConfigurator, IRabbitMqHost>
            registrationAction = null)
        {
            return Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri(Constants.RabbitMqUri), h =>
                    {
                        h.Username(Constants.UserName);
                        h.Password(Constants.Password);
                    });

                    registrationAction?.Invoke(cfg, host);
                });
        }
    }
}