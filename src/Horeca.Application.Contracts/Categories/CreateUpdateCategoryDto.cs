using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Horeca.Categories
{
    public class CreateUpdateCategoryDto
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
    }
}
