using System;
using Volo.Abp.Application.Dtos;

namespace Horeca.OrderLines
{
    public class OrderLineDto : EntityDto<Guid>
    {
        public Guid OrderId { get; set; }
        public Guid ProductBidId { get; set; }
        public string ProductName { get; set; }
        public string Supplier { get; set; }
        public int Count { get; set; }
        public double UnitPrice { get; set; }
        public double Sum
        {
            get
            {
                return UnitPrice * Count;
            }
        }
        public int MinAmount { get; set; }
        public int MaxAmount { get; set; }
    }
}
