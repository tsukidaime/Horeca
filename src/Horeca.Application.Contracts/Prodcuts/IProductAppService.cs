using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Horeca.Products
{
    public interface IProductAppService
        : ICrudAppService< 
            ProductDto, 
            Guid, //Primary key of the category entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateProductDto> //Used to create/update a category
    {
    }
}
