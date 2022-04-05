using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Horeca.Categories
{
    public class CategoryDto : EntityDto<Guid>
    {
        public string Name { get; set; }

        public string ParentName { get; set; }
        public IEnumerable<CategoryDto> Children { get; set; } 
    }
}
