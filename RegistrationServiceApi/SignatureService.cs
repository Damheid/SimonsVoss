using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Grpc.Net.Client;
using LicenseSignatureService;
using Microsoft.Extensions.Configuration;
using RegistrationLibrary;
using RegistrationLibrary.Interfaces;

namespace RegistrationServiceApi
{
    public class SignatureService : ISignatureService
    {
        private readonly IConfiguration configuration;
        public SignatureService(IConfiguration configuration)
        {
            this.configuration = configuration;

        }
        public async Task<string> Sign(RegistrationRequest request)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                // This switch must be set before creating the GrpcChannel/HttpClient (Because of macOS).
                // see https://docs.microsoft.com/en-us/aspnet/core/grpc/troubleshoot?view=aspnetcore-3.0#call-insecure-grpc-services-with-net-core-client
                AppContext.SetSwitch(
                    "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            }

            var address = configuration.GetValue<string>("SignatureServiceAddress");

            using var channel = GrpcChannel.ForAddress(address);

            var client = new LicenseSignature.LicenseSignatureClient(channel);
            var reply = await client.GenerateAsync(new SignatureRequest { LicenseKey = request.LicenseKey });

            return reply.Signature;
        }
    }
}