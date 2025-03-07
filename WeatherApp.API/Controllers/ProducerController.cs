using System.Text.Json;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace WeatherApp.API.Controllers;

[ApiController]
[Route("producerTest")]
public class ProducerController : ControllerBase
{
    private readonly string bootstrapServer = "kafka:29092";
    private readonly string topic = "test";
    
    [HttpPost]
    public async Task<IActionResult> Test([FromQuery] string name)
    {
        string message = JsonSerializer.Serialize(name);
        
        return Ok(await SendRequest(topic, message));
    }

    private async Task<bool> SendRequest(string topic, string message)
    {
        ProducerConfig config = new ProducerConfig()
        {
            BootstrapServers = bootstrapServer,
            Acks = Acks.All
        };

        try
        {
            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                var result = await producer.ProduceAsync(topic, new Message<Null, string> { Value = message });

                return await Task.FromResult(true);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return await Task.FromResult(false);
    }
}