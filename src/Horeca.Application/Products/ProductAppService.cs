using Horeca.Categories;
using Horeca.Models;
using Horeca.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Horeca.Products
{
    public class ProductAppService : CrudAppService<
            Product,
            ProductDto,
            Guid, 
            GetProductListDto, 
            CreateUpdateProductDto>, 
        IProductAppService
    {
        private readonly IRepository<Product, Guid> _productRepository;
        private ICategoryAppService _categoryAppService;

        public ProductAppService(IRepository<Product, Guid> repository,
            ICategoryAppService categoryAppService)
           : base(repository)
        {
            _productRepository = repository;
            _categoryAppService = categoryAppService;
            GetPolicyName = HorecaPermissions.ProductRead;
            CreatePolicyName = HorecaPermissions.ProductCreate;
            UpdatePolicyName = HorecaPermissions.ProductEdit;
            DeletePolicyName = HorecaPermissions.ProductDelete;
        }
        public override async Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
        {
            var product = await MapToEntityAsync(input);
            product.CreatorId = CurrentUser.Id;
            product.CreationTime = DateTime.Now;
            product = await _productRepository.InsertAsync(product);
            await _productRepository.EnsurePropertyLoadedAsync(product, p => p.Category);
            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        public override async Task<ProductDto> GetAsync(Guid id)
        {
            var product = await _productRepository.GetAsync(id);
            await _productRepository.EnsurePropertyLoadedAsync(product, p => p.Category);
            return ObjectMapper.Map<Product, ProductDto>(product);
        }

        public override async Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto input)
        {
            var query = await GetQueryAsync(input, !input.Name.IsNullOrEmpty(), x => x.Name.StartsWith(input.Name));
            query = await GetQueryAsync(input, input.CategoryId != null, x => x.CategoryId == input.CategoryId 
            || _categoryAppService.IsDescendant(x.CategoryId, (Guid)input.CategoryId).Result, query);
            if (input.OnlyApproved)
                query = query.Where(x => x.ApprovalState == ApprovalState.Approved);
            var totalCount = await query.CountAsync();
            query = query.Skip(input.SkipCount).Take(input.MaxResultCount);
            var products = await query.ToListAsync();

            return new PagedResultDto<ProductDto>(
                totalCount,
                ObjectMapper.Map<List<Product>, List<ProductDto>>(products)
            );
        }
        private async Task<IQueryable<Product>> GetQueryAsync(GetProductListDto input, bool filterExists, Expression<Func<Product, bool>> filter, IQueryable<Product> query = null)
        {
            query ??= await _productRepository.WithDetailsAsync(x => x.Category);
            if (filterExists)
                query = query.Where(filter);
            return query;
        }

        [Authorize(HorecaPermissions.ProductApprove)]
        public async Task<ProductDto> UpdateApprovalStateAsync(Guid id,  ApprovalState state)
        {
            var entity = await _productRepository.GetAsync(id);
            entity.ApprovalState = state;
            await _productRepository.UpdateAsync(entity);
            return await MapToGetOutputDtoAsync(entity);
        }
    }
}
