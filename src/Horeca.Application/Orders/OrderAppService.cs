using Horeca.Models;
using Horeca.Permissions;
using System;
using System.Threading.Tasks;
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

        private readonly IRepository<Order, Guid> _orderRepository;

        public OrderAppService(IRepository<Order, Guid> repository) : base(repository)
        {
            _orderRepository = repository;
            GetPolicyName = HorecaPermissions.OrderRead;
            CreatePolicyName = HorecaPermissions.OrderCreate;
            UpdatePolicyName = HorecaPermissions.OrderEdit;
            DeletePolicyName = HorecaPermissions.OrderDelete;
        }

        public async override Task<OrderDto> CreateAsync(CreateUpdateOrderDto input)
        {
            return MapToGetOutputDto(await _orderRepository.InsertAsync(MapToEntity(input)));
        }
    }
}
