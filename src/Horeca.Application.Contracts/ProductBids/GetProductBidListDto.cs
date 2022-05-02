using System;
using Volo.Abp.Application.Dtos;

namespace Horeca.ProductBids
{
    public class GetProductBidListDto : PagedAndSortedResultRequestDto
    {
        public Guid ProductId { get; set; }
    }
}
