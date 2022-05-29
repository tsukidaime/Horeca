using System;
using Volo.Abp.Application.Dtos;

namespace Horeca.Products
{
    public class GetProductListDto : PagedAndSortedResultRequestDto
    {
        public bool OnlyApproved { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? SuplierId { get; set; }
        public string Name { get; set; }
    }
}
