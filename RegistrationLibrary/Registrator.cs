using System.Threading.Tasks;
using RegistrationLibrary.Interfaces;

namespace RegistrationLibrary
{
    public class Registrator : IRegistrator
    {
        private readonly IRegistrationValidation validation;
        private readonly ISignatureService signatureService;

        public Registrator(IRegistrationValidation validation,
                           ISignatureService signatureService)
        {
            this.signatureService = signatureService;
            this.validation = validation;
        }

        public async Task<RegistrationResult> Register(RegistrationRequest request)
        {
            var result = new RegistrationResult { Success = false };
            
            if (validation.IsValid(request))
            {
                result.Signature = await signatureService.Sign(request);
                result.Success = true;
            }

            return result;
        }
    }
}