using System.Threading.Tasks;

namespace RegistrationLibrary.Interfaces
{
    public interface ISignatureService
    {
        Task<string> Sign(RegistrationRequest request);
    }
}