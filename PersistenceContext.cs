using AppBackendTraining.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppBackendTraining
{
    public class PersistenceContext: DbContext
    {
        public PersistenceContext(DbContextOptions<PersistenceContext> options): base(options)
        {
        }

        public DbSet<NotificationEntity> NotificationEntities{ get; set; }
    }
}
