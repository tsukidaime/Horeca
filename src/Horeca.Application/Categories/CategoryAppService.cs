using Horeca.Models;
using Horeca.Permissions;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Horeca.Categories
{
    public class CategoryAppService : CrudAppService<
            Category,
            CategoryDto,
            Guid, 
            PagedAndSortedResultRequestDto, 
            CreateUpdateCategoryDto>, 
        ICategoryAppService
    {
        private readonly IRepository<Category, Guid> _categoryRepository;

        public CategoryAppService(IRepository<Category, Guid> repository)
           : base(repository)
        {
            _categoryRepository = repository;
            GetPolicyName = HorecaPermissions.CategoryRead;
            CreatePolicyName = HorecaPermissions.CategoryCreate;
            UpdatePolicyName = HorecaPermissions.CategoryEdit;
            DeletePolicyName = HorecaPermissions.CategoryDelete;
        }
        public async override Task<CategoryDto> CreateAsync(CreateUpdateCategoryDto input)
        {
            return MapToGetOutputDto(await _categoryRepository.InsertAsync(MapToEntity(input)));
        }

        public async override Task DeleteAsync(Guid id)
        {
            await base.DeleteAsync(id);
        }

        public async override Task<CategoryDto> GetAsync(Guid id)
        {
            return await base.GetAsync(id);
        }

        public async override Task<PagedResultDto<CategoryDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return await base.GetListAsync(input);
        }

        public async override Task<CategoryDto> UpdateAsync(Guid id, CreateUpdateCategoryDto input)
        {
            return await base.UpdateAsync(id, input);
        }
    }
}
