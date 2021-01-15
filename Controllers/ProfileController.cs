using AppBackendTraining.Models.Profiles;
using AppBackendTraining.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AppBackendTraining.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {

        private readonly ILogger<ProfileController> _logger;
        private readonly ProfileService _profileService;

        public ProfileController(ILogger<ProfileController> logger, ProfileService profileService)
        {
            _logger = logger;
            _profileService = profileService;
        }

        [Authorize]
        [HttpPost]
        public async void CreateProfile(ProfileCreateRequest profileCreateRequest, long accountId)
        {
            await _profileService.CreateProfile(profileCreateRequest, accountId);
        }

    }
}
