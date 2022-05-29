using Horeca.Orders;
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
        public Guid? AddressId { get; set; }
        public OrderState OrderState { get; set; }
        public virtual Address Address { get; set; }
        public virtual IEnumerable<OrderLine> Lines { get; set; }
    }

    public class OrderLine : Entity<Guid>
    {
        public Guid OrderId { get; set; }
        public Guid ProductBidId { get; set; }
        public Guid SupplierId { get; set; }
        public int Count { get; set; }
        public double UnitPrice { get; set; }
        public virtual Order Order { get; set; }
        public virtual ProductBid ProductBid { get; set; }
    }

}
