using AppBackendTraining.Models.Account;
using AppBackendTraining.Models.Auth;
using AppBackendTraining.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AppBackendTraining.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {

        public Account Account => (Account)HttpContext.Items["Account"];
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;
        private readonly IAuthService _authenticationService;


        public AccountController(ILogger<AccountController> logger, IAccountService accountService, IAuthService authService)
        {
            _logger = logger;
            _accountService = accountService;
            _authenticationService = authService;
        }

        [HttpPost]
        public async void CreateAccount(AccountCreateRequest accountCreateRequest)
        {
            await _accountService.CreateAccount(accountCreateRequest);
        }

        [Authorize]
        [HttpGet]
        public async Task<AccountView> GetAccount()
        {
            return await _accountService.GetAccount(Account.PmsAccountId);
        }

        [HttpPost("authenticate")]
        public async Task<TokenView> Authenticate(AuthenticationRequest authenticateRequest)
        {
            return await _authenticationService.Authenticate(authenticateRequest);
        }

    }
}
