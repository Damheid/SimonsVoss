using System.Threading.Tasks;
using RegistrationService;

namespace RegistrationServiceApi.Interfaces
{
    public interface ISignatureService
    {
         Task<string> Sign(RegistrationRequest request);
    }
}