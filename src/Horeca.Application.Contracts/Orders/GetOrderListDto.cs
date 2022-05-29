using System;
using Volo.Abp.Application.Dtos;

namespace Horeca.Orders
{
    public class GetOrderListDto : PagedAndSortedResultRequestDto
    {
        public Guid? SupplierId { get; set; }
        public Guid? CustomerId { get; set; }
        public OrderState? OrderState { get; set; }
    }
}
