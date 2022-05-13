using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Horeca.Categories
{
    public interface ICategoryAppService
        : ICrudAppService< 
            CategoryDto, 
            Guid, //Primary key of the category entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateCategoryDto> //Used to create/update a category
    {
        Task<List<CategoryDto>> GetRootCategories();
        Task<bool> HasChild(Guid id);
        Task<List<CategoryDto>> GetChildren(Guid id);
        Task<bool> IsDescendant(Guid rootId, Guid categoryId);
    }
}
