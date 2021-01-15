using AppBackendTraining.Clients.Pms;
using AppBackendTraining.Helpers;
using AppBackendTraining.Models.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AppBackendTraining.Services
{
    public interface IAuthService
    {
        public Task<TokenView> Authenticate(AuthenticationRequest authenticateRequest);
    }

    public class AuthenticationService : IAuthService
    {

        private readonly IPmsService _pmsService;
        private readonly AppSettings _appSettings;

        public AuthenticationService(IPmsService pmsService, IOptions<AppSettings> appSettings)
        {
            _pmsService = pmsService;
            _appSettings = appSettings.Value;
        }

        public async Task<TokenView> Authenticate(AuthenticationRequest authenticateRequest)
        {
            var pmsAccount = await _pmsService.GetAccountByEmail(authenticateRequest.Email);

            // Since we are using hardcoded account, do not verify
            //
            //if(BC.Verify(authenticateRequest.Password, pmsAccount.PasswordHash))
            //{
            //    throw new UnauthorizedAccessException();
            //}

            var token = generateJwtToken(pmsAccount.AccountId);

            return new TokenView
            {
                Token = token
            };
        }

        // helper methods

        private string generateJwtToken(long accountId)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", accountId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
