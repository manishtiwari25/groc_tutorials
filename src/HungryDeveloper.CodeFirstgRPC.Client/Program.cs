using Grpc.Core;
using Grpc.Net.Client;
using HungryDeveloper.CodeFirstgRPC.Common.Contract;
using HungryDeveloper.CodeFirstgRPC.Common.Models;
using ProtoBuf.Grpc;
using ProtoBuf.Grpc.Client;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace HungryDeveloper.CodeFirstgRPC.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //await UnaryExample();
            // await ServerStreamingExample();
            //await ClientServerStreamingExample();
            await ClientStreamingExample();
        }
        static async Task ClientStreamingExample()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = channel.CreateGrpcService<IGreeterService>();
            var cancel = new CancellationTokenSource(TimeSpan.FromMinutes(1));

            var options = new CallOptions(cancellationToken: cancel.Token);
            var allAsync = GetStringsAsync();
            var res = await client.SayHelloClientStreamAsync(allAsync, new CallContext(options));
            Console.WriteLine($"Message: {res.Message}");

        }
        static async IAsyncEnumerable<HelloRequest> GetStringsAsync()
        {
            yield return new HelloRequest { Name = "Test 1" };
            await Task.Delay(1000);
            yield return new HelloRequest { Name = "Test 2" };
            await Task.Delay(1000);
            yield return new HelloRequest { Name = "Test 3" };
            await Task.Delay(1000);
            yield return new HelloRequest { Name = "Test 4" };
            await Task.Delay(1000);
            yield return new HelloRequest { Name = "Test 5" };
            await Task.Delay(1000);
            yield return new HelloRequest { Name = "Test 6" };
        }
        static async Task ClientServerStreamingExample()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = channel.CreateGrpcService<IGreeterService>();
            var cancel = new CancellationTokenSource(TimeSpan.FromMinutes(1));

            var options = new CallOptions(cancellationToken: cancel.Token);
            var allAsync = GetStringsAsync();
            await foreach (var res in client.SayHelloClientServerStreamAsync(allAsync, new CallContext(options)))
            {
                Console.WriteLine($"Message: {res.Message}");
            }
        }
        static async Task ServerStreamingExample()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = channel.CreateGrpcService<IGreeterService>();
            var cancel = new CancellationTokenSource(TimeSpan.FromMinutes(1));

            var options = new CallOptions(cancellationToken: cancel.Token);
            await foreach (var resp in client.SayHelloServerStreamAsync(new CallContext(options)))
            {
                Console.WriteLine($"Message: {resp.Message}");
            }
        }

        static async Task UnaryExample()
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = channel.CreateGrpcService<IGreeterService>();

            var reply = await client.SayHelloAsync(
                new HelloRequest { Name = "GreeterClient" });

            Console.WriteLine($"Greeting: {reply.Message}");
        }
    }
}
