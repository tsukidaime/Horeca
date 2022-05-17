using System;
using Volo.Abp.Application.Services;

namespace Horeca.Orders
{
    public interface IOrderAppService 
        : ICrudAppService<
            OrderDto,
            Guid,
            GetOrderListDto,
            CreateUpdateOrderDto>
    {
    }
}
