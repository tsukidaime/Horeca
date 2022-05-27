using System;
using Volo.Abp.Application.Dtos;

namespace Horeca.OrderLines
{
    public  class GetOrderLineListDto : PagedAndSortedResultRequestDto
    {
        public Guid? SupplierId { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? ProductId { get; set; }
    }
}
