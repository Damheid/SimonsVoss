using System.Threading.Tasks;

namespace RegistrationLibrary.Interfaces
{
    public interface IRegistrator
    {
         Task<RegistrationResult> Register(RegistrationRequest request);
    }
}