using System;
using System.Text.Json;
using CarvedRock.Api.ApiModels;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using Serilog;

namespace CarvedRock.Api.Integrations
{
    public class OrderProcessingNotification : IOrderProcessingNotification, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private string queueName = "quickorder.received";

        public OrderProcessingNotification(IConfiguration config)
        {
            var factory = new ConnectionFactory
            {
                HostName = config.GetValue<string>("RabbitMqHost")
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false,
                arguments: null);
        }

        public void QuickOrderReceived(QuickOrder order, int customerId, Guid orderId)
        {
            var message = new QuickOrderReceivedMessage {Order = order, CustomerId = customerId, OrderId = orderId};

            var messageBytes = JsonSerializer.SerializeToUtf8Bytes(message);
            _channel.BasicPublish("", routingKey: queueName, basicProperties:null, body: messageBytes);
            Log.ForContext("Body", message, true)
               .Information("Published quickorder notification");
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
