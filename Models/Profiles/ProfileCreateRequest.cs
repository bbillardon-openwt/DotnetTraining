
using System;
using System.ComponentModel.DataAnnotations;

namespace AppBackendTraining.Models.Profiles
{
    public class ProfileCreateRequest
    {
        [Required]
        public DateTime Birthdate { get; set; }
        [Required]
        public string InsuranceNumber  { get; set; }
        [Required]
        public string MemberNumber  { get; set; }
    }
}