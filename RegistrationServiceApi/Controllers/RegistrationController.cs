using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace RegistrationServiceApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        [HttpPost]
        public async Task<RegistrationResult> Post(RegistrationRequest request)
        {
            return new RegistrationResult();
        }

    }
}