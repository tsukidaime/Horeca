using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Horeca.Models;

namespace Horeca.ProductBids
{
    public class ProductBidAppService : CrudAppService<
            ProductBid,
            ProductBidDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateProductBidDto>,
        IProductBidAppService
    {
        private readonly IRepository<ProductBid, Guid> _repository;
        public ProductBidAppService(IRepository<ProductBid, Guid> repository) : base(repository)
        {
        }

    }
}
