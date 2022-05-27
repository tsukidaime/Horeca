using Horeca.Models;
using Horeca.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

namespace Horeca.OrderLines
{
    public class OrderLineAppService : CrudAppService<
        OrderLine,
        OrderLineDto,
        Guid,
        GetOrderLineListDto,
        CreateUpdateOrderLineDto>,
        IOrderLineAppService
    {
        private readonly IIdentityUserAppService _userService;
        private readonly IProductAppService _productAppService;

        public OrderLineAppService(IRepository<OrderLine, Guid> repository, 
            IIdentityUserAppService userService, IProductAppService productAppService) : base(repository)
        {
            _userService = userService;
            _productAppService = productAppService;
        }

        public override async Task<PagedResultDto<OrderLineDto>> GetListAsync(GetOrderLineListDto input)
        {
            var query = await Repository.WithDetailsAsync(x=>x.ProductBid);
            query = query.WhereIf(input.OrderId != null,x => x.OrderId == input.OrderId);
            query = query.WhereIf(input.SupplierId != null, x => x.SupplierId == input.SupplierId);
            query = query.Skip(input.SkipCount)
                .Take(input.MaxResultCount);
            var totalCount = await query.CountAsync();
            var lines = await query.ToListAsync();
            var linesDto = new List<OrderLineDto>();
            foreach (var line in lines)
            {
                var supplier = await _userService.GetAsync(line.SupplierId);
                var product = await _productAppService.GetAsync(line.ProductBid.ProductId);
                var lineDto = ObjectMapper.Map<OrderLine, OrderLineDto>(line);
                lineDto.MinAmount = line.ProductBid.MinPurchaseAmount;
                lineDto.MaxAmount = line.ProductBid.MaxPurchaseAmount;
                lineDto.ProductName = product.Name;
                lineDto.Supplier = supplier.GetProperty<string>("CompanyName");
                linesDto.Add(lineDto);
            }

            return new PagedResultDto<OrderLineDto>(
                totalCount,
                linesDto
                );
        }
    }
}
