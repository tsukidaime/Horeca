using Horeca.Models;
using Horeca.Permissions;
using System;
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
            PagedAndSortedResultRequestDto, 
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
        public async override Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
        {
            var product = MapToEntity(input);
            product.CreatorId = CurrentUser.Id;
            product.CreationTime = DateTime.Now;
            return MapToGetOutputDto(await _productRepository.InsertAsync(product));
        }

        public async override Task DeleteAsync(Guid id)
        {
            await base.DeleteAsync(id);
        }

        public async override Task<ProductDto> GetAsync(Guid id)
        {
            return await base.GetAsync(id);
        }

        public async override Task<PagedResultDto<ProductDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return await base.GetListAsync(input);
        }

        public async override Task<ProductDto> UpdateAsync(Guid id, CreateUpdateProductDto input)
        {
            return await base.UpdateAsync(id, input);
        }
    }
}
