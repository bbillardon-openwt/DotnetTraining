using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppBackendTraining.Entities
{
    public class NotificationEntity
    {
        public string Id { get; set; }
        public long AccountId { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
