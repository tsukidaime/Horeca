using System;
using Volo.Abp.Application.Services;

namespace Horeca.OrderLines
{ 
    public interface IOrderLineAppService : ICrudAppService<
        OrderLineDto,
        Guid,
        GetOrderLineListDto,
        CreateUpdateOrderLineDto>
    {
    }
}
