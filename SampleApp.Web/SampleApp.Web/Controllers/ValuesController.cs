using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SampleApp.Messaging;
using SampleApp.Web.ViewModels;

namespace SampleApp.Web.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        //https://aspnetmonsters.com/2017/03/2017-03-24-masstransit1/
        [HttpGet("Test")]
        public async Task TestMethod()
        {
            UserViewModel model =
                new UserViewModel
                {
                    FirstName = "Jan",
                    LastName = "Kowalski",
                    Address = "Street 23",
                    Age = 23,
                    IdNumber = 232323
                };

            var bus = BusConfigurator.ConfigureBus();

            var sendToUri = new Uri($"{Constants.RabbitMqUri}" + $"{Constants.RegisterUserServiceQueue}");
            var endPoint = await bus.GetSendEndpoint(sendToUri);


            // no need to have a class implementing interface ( as we needed one during the implementation for rabbitmq
            await endPoint.Send<IRegisterUserCommand>(
                new
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName, 
                    Address = model.Address,
                    Age = model.Age,
                    IdNumber = model.IdNumber
                });

        }
    }
}
