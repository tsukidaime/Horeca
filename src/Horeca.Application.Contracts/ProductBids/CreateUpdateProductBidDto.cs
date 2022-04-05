using System;
using System.ComponentModel.DataAnnotations;

namespace Horeca.ProductBids
{
    public class CreateUpdateProductBidDto
    {
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int MinPurchaseAmount { get; set; }
        [Required]
        public int MaxPurchaseAmount { get; set; }
        [Required]
        public Guid ProductId { get; set; }
    }
}
