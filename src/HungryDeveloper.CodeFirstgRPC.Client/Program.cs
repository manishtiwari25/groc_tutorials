using Grpc.Net.Client;
using HungryDeveloper.CodeFirstgRPC.Common.Contract;
using HungryDeveloper.CodeFirstgRPC.Common.Models;
using ProtoBuf.Grpc.Client;
using System;
using System.Threading.Tasks;

namespace HungryDeveloper.CodeFirstgRPC.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = channel.CreateGrpcService<IGreeterService>();

            var reply = await client.SayHelloAsync(
                new HelloRequest { Name = "GreeterClient" });

            Console.WriteLine($"Greeting: {reply.Message}");
        }
    }
}
