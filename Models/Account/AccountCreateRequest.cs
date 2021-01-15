
using System.ComponentModel.DataAnnotations;

namespace AppBackendTraining.Models.Account
{
    public class AccountCreateRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public GenderEnum Gender { get; set; }
        [Required]
        public LanguageEnum Language { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
    }
}