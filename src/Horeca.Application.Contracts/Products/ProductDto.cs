using System;
using Volo.Abp.Application.Dtos;

namespace Horeca.Products
{
    public class ProductDto : AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string CategoryName { get; set; }
        public ApprovalState ApprovalState { get; set; }
        public Guid CategoryId { get; set; }
    }
}
