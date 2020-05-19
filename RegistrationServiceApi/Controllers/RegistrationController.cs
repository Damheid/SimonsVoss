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
        private readonly IRegistrator registrator;
        public RegistrationController(IRegistrator registrator)
        {
            this.registrator = registrator;
        }

        [HttpGet]
        public string Get()
        {
            return "Registration Service";
        }

        [HttpPost]
        public async Task<RegistrationResult> Post([FromBody] RegistrationRequest request)
        {
            return await registrator.Register(request);
        }
    }
}