using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;

namespace Horeca.Models
{
    public class ProductBid : Entity<Guid>
    {
        public double Price { get; set; }
        public int MinPurchaseAmount { get; set; }
        public int MaxPurchaseAmount { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
    }
}
