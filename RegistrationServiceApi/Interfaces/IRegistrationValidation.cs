using RegistrationService;

namespace RegistrationServiceApi.Interfaces
{
    public interface IRegistrationValidation
    {
         bool IsValid(RegistrationRequest request);
    }
}