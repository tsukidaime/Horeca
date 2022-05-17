
using System;
using Volo.Abp.Application.Dtos;

namespace Horeca.Orders
{
    public class OrderDto : AuditedEntityDto<Guid>
    {
        public Guid UserId { get; set; }
    }
}
