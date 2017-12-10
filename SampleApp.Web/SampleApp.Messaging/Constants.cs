using System;
using System.Collections.Generic;
using System.Text;

namespace SampleApp.Messaging
{
    public static class Constants
    {
        public const string RabbitMqUri = "rabbitmq://localhost/userregistration/";
        public const string UserName = "guest";
        public const string Password = "guest";
        public const string RegisterUserServiceQueue = "registeruser.service";
        public const string NotificationServiceQueue = "notification.service";
        public const string FinanceServiceQueue = "finance.service";
    }
}
