using System;
using RegistrationLibrary;

namespace RegistrationPortal.Models
{
    [Serializable]
    public class ResultModel
    {
        public bool Success { get; set; }
        public string Signature { get; set; }
        public string Message { get; set; }

        public static ResultModel FromResult(RegistrationResult result)
        {
            var message = result.Success ? "Here is the signature of your Registration."
            : "It was not possible to sign your Registration.";

            return new ResultModel
            {
                Success = result.Success,
                Signature = result.Signature,
                Message = message
            };
        }
    }
}