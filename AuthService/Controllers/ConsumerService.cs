using System.Text.Json;
using AuthService.Services;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

public class ConsumerService(IServiceScopeFactory _serviceScopeFactory) : IHostedService
{
    private readonly string topic = "test";
    private readonly string groupId = "testGroup";
    private readonly string bootstrapServer = "kafka:29092";

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(() => ConsumeMessages(cancellationToken), cancellationToken);
        return Task.CompletedTask;
    }

    private async Task ConsumeMessages(CancellationToken cancellationToken)
    {
        var config = new ConsumerConfig()
        {
            GroupId = groupId,
            BootstrapServers = bootstrapServer,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        using (var consumerBuilder = new ConsumerBuilder<Ignore, string>(config).Build())
        {
            Console.WriteLine("Before subscription!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            Thread.Sleep(20000);
            consumerBuilder.Subscribe(topic);
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    var consumer = consumerBuilder.Consume(cancellationToken);
                    var result = JsonSerializer.Deserialize<string?>(consumer.Message.Value);
                    Console.WriteLine($"Received message: {result}");

                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var userService = scope.ServiceProvider.GetRequiredService<UserService>();
                        await userService.Register(result + " ", "test", "test");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Consumer was cancelled.");
            }
            finally
            {
                consumerBuilder.Close();
            }
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}