using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Horeca.Products
{
    public class CreateUpdateProductDto : EntityDto<Guid>
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
