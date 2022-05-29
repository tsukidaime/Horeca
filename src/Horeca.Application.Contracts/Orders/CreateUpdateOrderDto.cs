using System;
using Volo.Abp.Application.Dtos;

namespace Horeca.Orders
{
    public class CreateUpdateOrderDto : EntityDto<Guid>
    {
        public Guid UserId { get; set; }
        public Guid? AddressId { get; set; }
        public OrderState OrderState { get; set; }
    }
}
