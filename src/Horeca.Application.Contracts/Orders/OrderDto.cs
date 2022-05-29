
using Horeca.Addresses;
using Horeca.OrderLines;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Horeca.Orders
{
    public class OrderDto : AuditedEntityDto<Guid>
    {
        public string Customer { get; set; }
        public Guid UserId { get; set; }
        public OrderState OrderState { get; set; }
        public double Total { get; set; }
        public AddressDto AddressDto { get; set; }
        public List<OrderLineDto> Lines { get; set; }
    }
}
