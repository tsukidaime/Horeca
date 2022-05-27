using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
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
        Task<OrderDto> GetOrderByUserId(Guid userId);
    }
}
