using Horeca.Products;
using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities.Auditing;

namespace Horeca.Models
{
    public class Product : AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public ApprovalState ApprovalState { get; set; }
        public Guid CategoryId { get; set; }
        public List<ProductBid> ProductBids { get; set; }
    }
}
