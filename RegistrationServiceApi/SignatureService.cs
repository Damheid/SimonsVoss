using System;
using System.Threading.Tasks;
using Grpc.Net.Client;
using LicenseSignatureService;
using RegistrationService;
using RegistrationServiceApi.Interfaces;

namespace RegistrationServiceApi
{
    public class SignatureService : ISignatureService
    {
        public async Task<string> Sign(RegistrationRequest request)
        {
            // This switch must be set before creating the GrpcChannel/HttpClient (Because of macOS).
            // see https://docs.microsoft.com/en-us/aspnet/core/grpc/troubleshoot?view=aspnetcore-3.0#call-insecure-grpc-services-with-net-core-client
            AppContext.SetSwitch(
                "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            using var channel = GrpcChannel.ForAddress("http://localhost:5002");
            var client = new LicenseSignature.LicenseSignatureClient(channel);
            var reply = await client.GenerateAsync(new SignatureRequest { LicenseKey = request.LicenseKey });

            return reply.Signature;
        }
    }
}