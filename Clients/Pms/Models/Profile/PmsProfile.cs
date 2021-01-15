using System;

namespace AppBackendTraining.Clients.Pms.Models.Profile
{
    public class PmsProfile
    {
        public long ProfileId { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsPrimary { get; set; }
        public long AccountId { get; set; }
        public string VerificationState { get; set; }
    }
}