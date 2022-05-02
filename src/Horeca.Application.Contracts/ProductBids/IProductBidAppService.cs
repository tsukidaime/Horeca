using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Horeca.ProductBids
{
    public interface IProductBidAppService : ICrudAppService<
            ProductBidDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateProductBidDto>
    {
        Task<PagedResultDto<ProductBidDto>> GetListByProductIdAsync(GetProductBidListDto input);
    }
}
