using Horeca.Models;
using Horeca.Permissions;
using System;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Horeca.Orders
{
    public class OrderAppService : CrudAppService<
        Order,
        OrderDto,
        Guid,
        GetOrderListDto,
        CreateUpdateOrderDto>,
        IOrderAppService
    {
        public OrderAppService(IRepository<Order, Guid> repository) : base(repository)
        {
            GetPolicyName = HorecaPermissions.OrderRead;
            CreatePolicyName = HorecaPermissions.OrderCreate;
            UpdatePolicyName = HorecaPermissions.OrderEdit;
            DeletePolicyName = HorecaPermissions.OrderDelete;
        }
    }
}
