using System;
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

    }
}
