using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tools.Infrastructure.RabbitMQ
{
    public class BusFactory
    {
        /// <summary>
        /// Créé un bus
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="consumerName"></param>
        /// <returns></returns>
        public static IBus CreateBus(string connectionString, string consumerName)
        {
            var bus = RabbitHutch.CreateBus($"{connectionString};timeout=120");
            
            bus.Advanced.Container.Resolve<IConventions>().ErrorExchangeNamingConvention =
                info => $"{consumerName}_ErrorExchange";

            bus.Advanced.Container.Resolve<IConventions>().ErrorQueueNamingConvention =
                () => $"{consumerName}_ErrorQueue";

            return bus;
        }
    }
}
