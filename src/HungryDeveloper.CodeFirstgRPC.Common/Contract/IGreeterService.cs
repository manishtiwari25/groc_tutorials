using Grpc.Core;
using HungryDeveloper.CodeFirstgRPC.Common.Models;
using ProtoBuf.Grpc;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace HungryDeveloper.CodeFirstgRPC.Common.Contract
{
    [ServiceContract]
    public interface IGreeterService
    {
        [OperationContract]
        Task<HelloReply> SayHelloAsync(HelloRequest request, CallContext context = default);

        [OperationContract]
        IAsyncEnumerable<HelloReply> SayHelloServerStreamAsync(CallContext context = default);

        [OperationContract]
        IAsyncEnumerable<HelloReply> SayHelloClientServerStreamAsync(IAsyncEnumerable<HelloRequest> request, CallContext context = default);

        [OperationContract]
        Task<HelloReply> SayHelloClientStreamAsync(IAsyncEnumerable<HelloRequest> request, CallContext context = default);
    }
}
