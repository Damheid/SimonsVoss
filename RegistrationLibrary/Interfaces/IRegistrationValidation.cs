namespace RegistrationLibrary.Interfaces
{
    public interface IRegistrationValidation
    {
        bool IsValid(RegistrationRequest request);
    }
}