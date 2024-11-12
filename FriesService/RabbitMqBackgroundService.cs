﻿
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using FriesService.Models;
using System.Text.Json;
using System.Text;
using OrderLibrary.Models;

namespace FriesService
{
    public class RabbitMqBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public RabbitMqBackgroundService (IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() => {

                var factory = new ConnectionFactory()
                {
                    HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost",
                    UserName = Environment.GetEnvironmentVariable("RABBITMQ_USER") ?? "guest",
                    Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "guest"
                };

                var connection = factory.CreateConnection();
                var channel = connection.CreateModel();

                channel.QueueDeclare(queue: "fries", durable: false, exclusive: false, autoDelete: false, arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += async (model, ea) =>
                {
                    using (var scope = _serviceProvider.CreateScope()) {

                        var context = scope.ServiceProvider.GetRequiredService<DbContextSettings>();

                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);
                        var order = JsonSerializer.Deserialize<Order>(message);

                        if (order != null)
                        {
                            context.Orders.Add(order);
                            await context.SaveChangesAsync();
                            Console.WriteLine($"Received and saved order for {order.Quantity} of {order.ItemName}");
                        }
                    }
                };

                channel.BasicConsume(queue: "fries", autoAck: true, consumer: consumer);

                while (!stoppingToken.IsCancellationRequested) {
                    Thread.Sleep(1000);
                }

            }, stoppingToken);
        }
    }
}
