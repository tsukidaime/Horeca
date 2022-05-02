using Volo.Abp.Application.Dtos;

namespace Horeca.Products
{
    public class GetProductListDto : PagedAndSortedResultRequestDto
    {
        public bool OnlyApproved { get; set; }
    }
}
