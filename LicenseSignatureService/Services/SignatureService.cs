using System.Threading.Tasks;

namespace LicenseSignatureService
{
    public class SignatureService : LicenseSignature.LicenseSignatureBase
    {
        public override Task<SignatureResponse> Generate(SignatureRequest request, Grpc.Core.ServerCallContext context)
        {
            return Task.FromResult(new SignatureResponse { Signature = "ABCD" });
        }
    }
}