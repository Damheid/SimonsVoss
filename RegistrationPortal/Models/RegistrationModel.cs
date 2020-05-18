
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RegistrationLibrary;

namespace RegistrationPortal.Models
{
    public class RegistrationModel
    {
        [DisplayName("Company Name")]
        [Required(ErrorMessage = "Company Name is required")]
        public string CompanyName { get; set; }

        [DisplayName("Contact Name")]
        [Required(ErrorMessage = "Contact is required")]
        public string ContactPerson { get; set; }

        [DisplayName("Contact Email")]
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [DisplayName("Company Address")]
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [DisplayName("License Key")]
        [Required(ErrorMessage = "License Key is required")]
        public string LicenseKey { get; set; }

        public RegistrationRequest ToRequest()
        {
            return new RegistrationRequest
            {
                CompanyName = this.CompanyName,
                ContactPerson = this.ContactPerson,
                Address = this.Address,
                Email = this.Email,
                LicenseKey = this.LicenseKey
            };
        }
    }
}