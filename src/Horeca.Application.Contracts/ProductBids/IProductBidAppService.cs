using System;
using System.Collections.Generic;
using System.Text;
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
    }
}
