using System;
using System.Collections.Generic;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace Horeca.Models
{
    public class Order : AuditedAggregateRoot<Guid>
    {
        public Guid UserId { get; set; }
        public IdentityUser User { get; set; }
        public virtual IEnumerable<OrderLine> Lines { get; set; }
    }

    public class OrderLine : Entity<Guid>
    {
        public Order Order { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }
        public double UnitPrice { get; set; }
        public virtual Product Product { get; set; }
    }

}
