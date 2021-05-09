using System;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CarvedRock.OrderProcessor.Models;
using CarvedRock.OrderProcessor.Repository;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;

namespace CarvedRock.OrderProcessor
{
    public class Worker : BackgroundService
    {
        private readonly IInventoryRepository _repo;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private string queueName = "quickorder.received";
        private EventingBasicConsumer _consumer;

        public Worker(IConfiguration config, IInventoryRepository repo)
        {
            _repo = repo;
            var factory = new ConnectionFactory
            {
                HostName = config.GetValue<string>("RabbitMqHost")
            };
            try
            {
                _connection = factory.CreateConnection();
            }
            catch (Exception)
            {
                for (var i = 0; i < 5; i++)
                {
                    if (_connection != null)
                        continue;
                    Thread.Sleep(3000);
                    try { _connection = factory.CreateConnection(); } catch { }
                }
                if (_connection == null) throw;
            }
            
            _channel = _connection.CreateModel();
            _channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false,
                arguments: null);

            _consumer = new EventingBasicConsumer(_channel);
            _consumer.Received += ProcessQuickOrderReceived;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _channel.BasicConsume(queue: queueName, autoAck: true, consumer: _consumer);
            }

            return Task.CompletedTask;
        }

        private async void ProcessQuickOrderReceived(object sender, BasicDeliverEventArgs eventArgs)
        {
            var orderInfo = JsonSerializer.Deserialize<QuickOrderReceivedMessage>(eventArgs.Body.ToArray());

            Log.ForContext("OrderReceived", orderInfo, true)
                .Information("Received message from queue for processing.");

            var currentQuantity = await _repo.GetInventoryForProduct(orderInfo.Order.ProductId);
            await _repo.UpdateInventoryForProduct(orderInfo.Order.ProductId,
                currentQuantity - orderInfo.Order.Quantity);
        }
    }
}
