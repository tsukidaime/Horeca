using Horeca.Models;
using Horeca.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public ProductAppService(IRepository<Product, Guid> repository)
           : base(repository)
        {
            _productRepository = repository;
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

        public async Task<PagedResultDto<ProductDto>> GetListByCategoryAsync(Guid? categoryId, GetProductListDto input)
        {
            return await GetListAsync(input, categoryId != null, x => x.CategoryId == categoryId);
        }

        private async Task<PagedResultDto<ProductDto>> GetListAsync(GetProductListDto input, bool filterExists, Expression<Func<Product, bool>> filter)
        {
            var query = await _productRepository.WithDetailsAsync(x => x.Category);
            if (input.OnlyApproved)
                query = query.Where(x => x.ApprovalState == ApprovalState.Approved);
            if (filterExists)
                query = query.Where(filter);
            query = query.Skip(input.SkipCount).Take(input.MaxResultCount);
            var products = await query.ToListAsync();

            var totalCount = filterExists
                ? await _productRepository.CountAsync()
                : await _productRepository.CountAsync(filter);

            return new PagedResultDto<ProductDto>(
                totalCount,
                ObjectMapper.Map<List<Product>, List<ProductDto>>(products)
            );
        }

        public async Task<PagedResultDto<ProductDto>> GetListByNameAsync(string name, GetProductListDto input)
        {
            return await GetListAsync(input, !name.IsNullOrEmpty(), x => x.Name.StartsWith(name));
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
