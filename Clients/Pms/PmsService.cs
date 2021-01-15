using AppBackendTraining.Clients.Pms.Models.Account;
using AppBackendTraining.Clients.Pms.Models.Profile;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using RestSharp;
using Microsoft.Extensions.Options;
using AppBackendTraining.Helpers;
using System.Collections.Generic;

namespace AppBackendTraining.Clients.Pms
{
    public interface IPmsService
    {
        public Task<PmsAccount> CreateAccount(PmsAccount accountCreateRequest);
        public Task<PmsAccount> GetAccountByEmail(string email);
        public Task<PmsAccount> GetAccountById(long id);
        public Task<PmsProfile> CreateProfile(PmsProfileCreateRequest profileCreateRequest, long accountId);
        public Task<IEnumerable<PmsProfile>> GetProfilesByAccountId(long accountId);

    }


    public class FakePmsService : IPmsService
    {
        private PmsAccount fakeAccount = new PmsAccount
        {
            AccountId = 1L,
            Email = "john@email.com",
            Language = "en",
            PasswordHash = "nkafklnsfa",
            PhoneNumber = "0787566072",
            FamilyName = "John",
            GivenName = "Johnson",
            Gender = "M"
        };


        private PmsProfile fakeProfile = new PmsProfile
        {
            AccountId = 1L,
            FamilyName = "John",
            GivenName = "Johnson",
            Gender = "M",
            BirthDate = DateTime.Now
        };
        public Task<PmsAccount> CreateAccount(PmsAccount accountCreateRequest)
        {
            return Task.FromResult(fakeAccount);
        }

        public Task<PmsProfile> CreateProfile(PmsProfileCreateRequest profileCreateRequest, long accountId)
        {
            return Task.FromResult(fakeProfile);
        }

        public Task<PmsAccount> GetAccountByEmail(string email)
        {
            return Task.FromResult(fakeAccount);
        }

        public Task<PmsAccount> GetAccountById(long id)
        {
            return Task.FromResult(fakeAccount);
        }

        public Task<IEnumerable<PmsProfile>> GetProfilesByAccountId(long accountId)
        {
            IEnumerable<PmsProfile> profiles = new List<PmsProfile>() { fakeProfile };
            return Task.FromResult(profiles) ;
        }
    }

    public class PmsService: IPmsService
    {
        private RestClient Client;


        public PmsService(IOptions<AppSettings> appSettings) {
            Client = new RestClient(appSettings.Value.PmsUrl);
        }

        public async Task<PmsAccount> CreateAccount(PmsAccount accountCreateRequest)
        {
            var request = new RestRequest("v1/accounts", Method.POST)
                .AddJsonBody(accountCreateRequest);

            var response = await Client.PostAsync<PmsAccount>(request);

            return response;
        }


        public async Task<PmsAccount> GetAccountByEmail(string email)
        {
            var response = await Client.GetAsync<PmsAccount>(
                           new RestRequest("v1/accounts")
                               .AddParameter("email", email));
            return response;
        }
        public async Task<PmsAccount> GetAccountById(long id)
        {
            var response = await Client.GetAsync<PmsAccount>(
                           new RestRequest($"v1/accounts/${id}"));
            return response;
        }

        public async Task<PmsProfile> CreateProfile(PmsProfileCreateRequest profileCreateRequest, long accountId)
        {
            var request = new RestRequest($"v1/accounts/{accountId}/profiles", Method.POST)
                .AddJsonBody(profileCreateRequest);

            var response = await Client.PostAsync<PmsProfile>(request);

            return response;
        }

        public async Task<IEnumerable<PmsProfile>> GetProfilesByAccountId(long accountId)
        {
            var response = await Client.GetAsync<IEnumerable<PmsProfile>>(
                new RestRequest($"v1/accounts/{accountId}/profiles"));
            return response;
        }
    }
}
