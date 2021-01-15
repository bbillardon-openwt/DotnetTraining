using AppBackendTraining.Clients.Pms;
using AppBackendTraining.Clients.Pms.Models.Account;
using AppBackendTraining.Helpers;
using AppBackendTraining.Models;
using AppBackendTraining.Models.Account;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace AppBackendTraining.Services
{
    public interface IAccountService
    {
        public Task<Account> GetAccountById(long id);
        public Task CreateAccount(AccountCreateRequest accountCreateRequest);
        public Task<AccountView> GetAccount(long id);
    }

    public class AccountService: IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly IPmsService _pmsService;
        public AccountService(ILogger<AccountService> logger, IPmsService pmsService)
        {
            _logger = logger;
            _pmsService = pmsService;
        }

        public async Task CreateAccount(AccountCreateRequest accountCreateRequest)
        {
            var pmsAccount = await _pmsService.CreateAccount(
                new PmsAccount
                {
                    Email = accountCreateRequest.Email,
                    Language = accountCreateRequest.Language.ToString(),
                    PasswordHash = BC.HashPassword(accountCreateRequest.Password),
                    PhoneNumber = accountCreateRequest.PhoneNumber,
                    FamilyName = accountCreateRequest.LastName,
                    GivenName = accountCreateRequest.FirstName,
                    Gender = accountCreateRequest.Gender.ToString()
                });


            return;
        }

        
        public async Task<Account> GetAccountById(long id)
        {
            var pmsAccount = await _pmsService.GetAccountById(id);
            var account = new Account
            {
                Email = pmsAccount.Email,
                PmsAccountId = pmsAccount.AccountId

            };

            return account;
        }

        public async Task<AccountView> GetAccount(long id)
        {
            var pmsAccount = await _pmsService.GetAccountById(id);

            return new AccountView
            {
                Email = pmsAccount.Email,
                Language = Enum.Parse<LanguageEnum>(pmsAccount.Language),
                PhoneNumber = pmsAccount.PhoneNumber
            };
        }

        
    }
}