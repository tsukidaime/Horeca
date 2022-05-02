using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Horeca.ProductBids
{
    public class ProductBidDto : EntityDto<Guid>
    {
        public double Price { get; set; }
        public int MinPurchaseAmount { get; set; }
        public int MaxPurchaseAmount { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
    }
}
