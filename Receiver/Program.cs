using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

namespace Receiver
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new ServiceBusClient("connectionString");
            var processorOptions = new ServiceBusProcessorOptions
            {
                MaxConcurrentCalls = 1,
                AutoCompleteMessages = false
            };

            await using ServiceBusProcessor processor = client.CreateProcessor("Queue or Topic", processorOptions);
            processor.ProcessMessageAsync += MessageHandler;
            processor.ProcessErrorAsync += ErrorHandler;
            await processor.StartProcessingAsync();
            Console.Read();
            await processor.CloseAsync();

        }
        private static async Task MessageHandler(ProcessMessageEventArgs arg)
        {
            string body = arg.Message.Body.ToString();
            Console.WriteLine($"Received: {body}");
            //message is deleted from the queue. 
            await arg.CompleteMessageAsync(arg.Message);

        }
        private static Task ErrorHandler(ProcessErrorEventArgs arg)
        {
            Console.WriteLine(arg.Exception.ToString());
            return Task.CompletedTask;
        }

       
    }
}
