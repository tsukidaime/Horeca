using Horeca.Models;
using Horeca.OrderLines;
using Horeca.Permissions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Data;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;

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

        private readonly IIdentityUserAppService _userService;
        private readonly IOrderLineAppService _lineService;
        private readonly IRepository<Order, Guid> _orderRepository;

        public OrderAppService(IRepository<Order, Guid> repository, 
            IIdentityUserAppService userService, IOrderLineAppService lineService) : base(repository)
        {
            _userService = userService;
            _lineService = lineService;
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

        public override async Task<PagedResultDto<OrderDto>> GetListAsync(GetOrderListDto input)
        {
            var query = await Repository.WithDetailsAsync(x => x.Lines);
            query = query.WhereIf(input.OrderState != null, x => x.OrderState == input.OrderState);
            query = query.WhereIf(input.SupplierId != null, x => x.Lines.Any(y=>y.SupplierId == input.SupplierId));

            var totalCount = await query.CountAsync();
            var orders = await query.ToListAsync();
            var ordersDto = new List<OrderDto>();
            foreach (var order in orders)
            {
                var customer = await _userService.GetAsync(order.UserId);
                var orderDto = ObjectMapper.Map<Order, OrderDto>(order);
                orderDto.Customer = customer.GetProperty<string>("CompanyName");
                orderDto.Total = order.Lines.Sum(x => x.UnitPrice * x.Count);
                var lines = await _lineService.GetListAsync(new GetOrderLineListDto
                {
                    MaxResultCount = 1000,
                    SkipCount = 0,
                    OrderId = orderDto.Id,
                    SupplierId = customer.Id
                });
                orderDto.Lines = lines.Items.ToList();
                ordersDto.Add(orderDto);
            }

            return new PagedResultDto<OrderDto>(
                totalCount,
                ordersDto
                );
        }

        public async Task<OrderDto> GetOrderByUserId(Guid userId)
        {
            var order = await _orderRepository.FirstOrDefaultAsync(x => x.UserId == userId && x.OrderState == OrderState.New);
            return ObjectMapper.Map<Order, OrderDto>(order);
        }
    }
}
