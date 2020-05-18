using System;
using System.Text;
using System.Threading.Tasks;

namespace LicenseSignatureService
{
    public class SignatureService : LicenseSignature.LicenseSignatureBase
    {
        public override Task<SignatureResponse> Generate(SignatureRequest request, Grpc.Core.ServerCallContext context)
        {
            var byteString = Sign(request.LicenseKey);
            return Task.FromResult(new SignatureResponse { Signature = Convert.ToBase64String(byteString) });
        }

        public byte[] Sign(string licenseKey)
        {
            return Encoding.UTF8.GetBytes(licenseKey);

            // Perform Sign algorithm here
        }
    }
}