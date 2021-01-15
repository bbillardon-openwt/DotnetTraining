using AppBackendTraining.Entities;
using AppBackendTraining.Models.Account;
using AppBackendTraining.Models.Profiles;
using AppBackendTraining.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppBackendTraining.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NotificationController : ControllerBase
    {
        private Account Account => (Account)HttpContext.Items["Account"];
        private readonly ILogger<NotificationController> _logger;
        private readonly PersistenceContext _persistenceContext;

        public NotificationController(ILogger<NotificationController> logger, PersistenceContext persistenceContext)
        {
            _logger = logger;
            _persistenceContext = persistenceContext;
        }

        [Authorize]
        [HttpGet]
        public async Task<IEnumerable<NotificationEntity>> GetNotifications()
        {
            return await _persistenceContext.NotificationEntities
                .Where(n => n.AccountId == Account.PmsAccountId)
                .ToListAsync();
        }

    }
}
