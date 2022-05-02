using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Horeca.Models;
using Microsoft.EntityFrameworkCore;
using Horeca.Permissions;

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
            _repository = repository;
            GetPolicyName = HorecaPermissions.ProductBidRead;
            CreatePolicyName = HorecaPermissions.ProductBidCreate;
            UpdatePolicyName = HorecaPermissions.ProductBidEdit;
            DeletePolicyName = HorecaPermissions.ProductBidDelete;
        }

        public override async Task<ProductBidDto> CreateAsync(CreateUpdateProductBidDto input)
        {
            var productBid = await MapToEntityAsync(input);
            productBid.UserId = (Guid)CurrentUser.Id;
            productBid = await _repository.InsertAsync(productBid);
            return ObjectMapper.Map<ProductBid, ProductBidDto>(productBid);
        }

        public async Task<PagedResultDto<ProductBidDto>> GetListByProductIdAsync(GetProductBidListDto input)
        {
            var query = await _repository.GetQueryableAsync();
            query = query.Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .Where(x => x.ProductId == input.ProductId);

            var bids = await query.ToListAsync();

            var totalCount = await _repository.CountAsync(x => x.ProductId == input.ProductId);

            return new PagedResultDto<ProductBidDto>(
                totalCount,
                ObjectMapper.Map<List<ProductBid>, List<ProductBidDto>>(bids)
                );
        }
    }
}
