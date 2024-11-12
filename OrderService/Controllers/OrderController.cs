using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;
using OrderLibrary.Models;

namespace OrderService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            HashSet<string> kitchenValidAreas = new HashSet<string>() { "fries", "grill", "salad", "drink", "desert" };

            if (!kitchenValidAreas.Contains(order.KitchenArea))
            {
                return BadRequest("This area of the kitchen do not exist");
            }

            if(order.ItemName == "" ||  order.ItemName == null)
            {
                return BadRequest("The order must have the item name");
            }

            if(order.Quantity <= 0)
            {
                return BadRequest("The order must have at least one item");
            }

            var factory = new ConnectionFactory()
            {
                HostName = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost",
                UserName = Environment.GetEnvironmentVariable("RABBITMQ_USER") ?? "guest",
                Password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "guest"
            };

            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: order.KitchenArea, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var message = JsonSerializer.Serialize(order);
            var body = Encoding.UTF8.GetBytes(message);

            //var properties = channel.CreateBasicProperties();

            //properties.Persistent = true;

            channel.BasicPublish(exchange: "", routingKey: order.KitchenArea, basicProperties: null, body: body);

            return Ok("Order sent to the kitchen!");
        }
    }
}
