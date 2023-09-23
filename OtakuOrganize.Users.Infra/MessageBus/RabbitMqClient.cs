using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OtakuOrganize.Users.Infra.MessageBus
{
    public class RabbitMqClient : IMessageBusClient
    {
        public readonly IConnection connection;
        public RabbitMqClient(ProducerConnection _producerConnection)
        {
            connection = _producerConnection.Connection;
        }
        public void Publish(object message, string routingKey, string exchange)
        {
            var channel = connection.CreateModel();

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()

            };
            var payload = JsonConvert.SerializeObject(message,settings);

            var body = Encoding.UTF8.GetBytes(payload);
             


            channel.ExchangeDeclare(exchange, "topic", true);
            var queueName = exchange + '/' + routingKey;
            channel.QueueDeclare(queue: queueName,
                     durable: false,
                     exclusive: false,
                     autoDelete: false,
                     arguments: null);

            channel.QueueBind(queue: queueName, exchange: exchange, routingKey: routingKey);
            channel.BasicPublish(exchange, routingKey,null,body: body);


            Console.WriteLine($"Message has been sending");

        }
    }
}
