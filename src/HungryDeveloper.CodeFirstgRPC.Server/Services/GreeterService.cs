using Grpc.Core;
using HungryDeveloper.CodeFirstgRPC.Common.Contract;
using HungryDeveloper.CodeFirstgRPC.Common.Models;
using Microsoft.Extensions.Logging;
using ProtoBuf.Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HungryDeveloper.CodeFirstgRPC.Server
{
    public class GreeterService : IGreeterService
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public Task<HelloReply> SayHelloAsync(HelloRequest request, CallContext context = default)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }

        public async IAsyncEnumerable<HelloReply> SayHelloClientServerStreamAsync(IAsyncEnumerable<HelloRequest> request, CallContext context = default)
        {
            await foreach (var req in request)
            {
                Console.WriteLine($"Name: {req.Name}");
                yield return new HelloReply
                {
                    Message = $"Hello   {req.Name}"
                };
            }
        }

        public async Task<HelloReply> SayHelloClientStreamAsync(IAsyncEnumerable<HelloRequest> request, CallContext context = default)
        {
            var concatString = string.Empty;
            await foreach (var req in request)
            {
                concatString += $" {req.Name }";
            }
            return new HelloReply
            {
                Message = $"Hello   {concatString}"
            };
        }

        public async IAsyncEnumerable<HelloReply> SayHelloServerStreamAsync(CallContext context = default)
        {
            for (int i = 0; i < 100; i++)
            {
                await Task.Delay(TimeSpan.FromSeconds(10));
                yield return new HelloReply
                {
                    Message = $"Hello   {i}"
                };
            }

        }
    }
}
