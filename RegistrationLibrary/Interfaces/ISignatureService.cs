using System.Threading.Tasks;
using RegistrationService;

namespace RegistrationLibrary.Interfaces
{
    public interface ISignatureService
    {
        Task<string> Sign(RegistrationRequest request);
    }
}