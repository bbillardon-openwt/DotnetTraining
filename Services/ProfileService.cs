using AppBackendTraining.Clients.Pms;
using AppBackendTraining.Clients.Pms.Models.Profile;
using AppBackendTraining.Models.Profiles;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AppBackendTraining.Services
{
    public class ProfileService
    {
        private readonly ILogger<ProfileService> _logger;
        private readonly PmsService _pmsService;
        public ProfileService(ILogger<ProfileService> logger, PmsService pmsService)
        {
            _logger = logger;
            _pmsService = pmsService;
        }

        public async Task CreateProfile(ProfileCreateRequest profileCreateRequest, long accountId)
        {
            await _pmsService.CreateProfile(
                new PmsProfileCreateRequest
                {
                    Birthdate = profileCreateRequest.Birthdate,
                    CardNumber = profileCreateRequest.InsuranceNumber
                },
                accountId);
        }
    }
}