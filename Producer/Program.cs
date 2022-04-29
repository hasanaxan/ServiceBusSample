using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
namespace Producer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new ServiceBusClient("connectionString");
            ServiceBusSender sender = client.CreateSender("Queue or Topic");
            string messageContent = "Test message.";
            var message = new ServiceBusMessage(messageContent);
            await sender.SendMessageAsync(message);
        }
    }
}
