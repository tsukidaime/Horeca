using Horeca.Models;
using Horeca.Permissions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<List<CategoryDto>> GetChildren(Guid id)
        {
            var category = await _categoryRepository.GetAsync(id, true);
            return ObjectMapper.Map<List<Category>, List<CategoryDto>>(category.SubCategories);
        }

        public async Task<bool> IsDescendant(Guid rootId, Guid categoryId)
        {
            var query = await _categoryRepository.GetQueryableAsync();
            var subItems = new List<bool>();
            foreach (var item in query.Where(x => x.ParentId == rootId))
            {
                subItems.Add(await IsDescendant(item.Id, categoryId) || rootId == item.ParentId);
            }
            return subItems.Any(x=>x==true);
        }

        public async Task<List<CategoryDto>> GetRootCategories()
        {
            var query = await _categoryRepository.WithDetailsAsync(x=>x.SubCategories);
            var categories = await AsyncExecuter.ToListAsync(query);
            return ObjectMapper.Map<List<Category>, List<CategoryDto>>(categories.Where(x => x.ParentId == null).ToList());
        }

        public async Task<bool> HasChild(Guid id)
        {
            return await _categoryRepository.AnyAsync(x => x.ParentId == id);
        }
    }
}
