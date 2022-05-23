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
using Volo.Abp.Identity;
using Volo.Abp.Data;

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
        private readonly IIdentityUserAppService _userService;
        public ProductBidAppService(IRepository<ProductBid, Guid> repository, IIdentityUserAppService userService) : base(repository)
        {
            _repository = repository;
            _userService = userService;
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
            var bidsDto = new List<ProductBidDto>();
            foreach(var bid in bids)
            {
                var bidDto = ObjectMapper.Map<ProductBid, ProductBidDto>(bid);
                var supplier = await _userService.GetAsync(bid.UserId);
                bidDto.SupplierName = supplier.GetProperty<string>("CompanyName");
                bidsDto.Add(bidDto);
            }
            var totalCount = await _repository.CountAsync(x => x.ProductId == input.ProductId);

            return new PagedResultDto<ProductBidDto>(
                totalCount,
                bidsDto
                );
        }
    }
}
