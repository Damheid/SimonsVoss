using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RegistrationLibrary;
using RegistrationLibrary.Interfaces;

namespace RegistrationServiceApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationValidation validation;
        private readonly ISignatureService signatureService;

        public RegistrationController(IRegistrationValidation validation,
                                      ISignatureService signatureService)
        {
            this.validation = validation;
            this.signatureService = signatureService;
        }

        [HttpPost]
        public async Task<RegistrationResult> Post([FromBody] RegistrationRequest request)
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