using RegistrationLibrary.Interfaces;
using RegistrationService;

namespace RegistrationServiceApi
{
    public class RegistrationValidation : IRegistrationValidation
    {
        public bool IsValid(RegistrationRequest request)
        {
            // This implementation is just and example.
            // specially with email and address.
            if (string.IsNullOrEmpty(request.CompanyName))
                return false;
            if (string.IsNullOrEmpty(request.ContactPerson))
                return false;
            if (string.IsNullOrEmpty(request.Email))
                return false;
            if (string.IsNullOrEmpty(request.Address))
                return false;
            if (string.IsNullOrEmpty(request.LicenseKey))
                return false;

            return true;
        }
    }
}