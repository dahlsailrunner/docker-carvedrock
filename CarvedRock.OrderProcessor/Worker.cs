using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CarvedRock.OrderProcessor.Models;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;

namespace CarvedRock.OrderProcessor
{
    public class Worker : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private string queueName = "quickorder.received";
        private EventingBasicConsumer _consumer;

        public Worker(IConfiguration config)
        {
            var factory = new ConnectionFactory
            {
                HostName = config.GetValue<string>("RabbitMqHost")
            };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false,
                arguments: null);

            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += ProcessQuickOrderReceived;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _channel.BasicConsume(queue: queueName, autoAck: true, consumer: _consumer);
            }
        }

        private void ProcessQuickOrderReceived(object sender, BasicDeliverEventArgs eventArgs)
        {
            var orderInfo = JsonSerializer.Deserialize<QuickOrderReceivedMessage>(eventArgs.Body.ToArray());

            Log.ForContext("OrderReceived", orderInfo, true)
                .Information("Received message from queue for processing.");
        }
    }
}
