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
            var enity = MapToEntity(input);
            enity.OrderState = OrderState.New;
            return MapToGetOutputDto(await _orderRepository.InsertAsync(enity));
        }

        public async Task<OrderDto> GetOrderByUserId(Guid userId)
        {
            var order = await _orderRepository.FirstOrDefaultAsync(x => x.UserId == userId && x.OrderState == OrderState.New);
            return ObjectMapper.Map<Order, OrderDto>(order);
        }
    }
}
