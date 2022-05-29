using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Blazorise;
using Blazorise.DataGrid;
using Horeca.Permissions;
using Horeca.Orders;
using Volo.Abp.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Horeca.Addresses;

namespace Horeca.Blazor.Pages.Order
{
    public partial class List
    {
        private IReadOnlyList<OrderDto> OrderList { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }
        private bool IsSupplier { get; set; }
        private bool IsCustomer { get; set; }
        private OrderState SelectedOrderStateFilter { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await SetPermissionsAsync();
            await GetOrdersAsync(OrderState.Submitted);
        }

        private async Task SelectOrderState(OrderState orderState)
        {
            await GetOrdersAsync(orderState);
            await InvokeAsync(StateHasChanged);
        }

        private async Task SetPermissionsAsync()
        {
            IsSupplier = await AuthorizationService
                .IsGrantedAsync(HorecaPermissions.OrderManagementSupplier);
            IsCustomer = await AuthorizationService
                .IsGrantedAsync(HorecaPermissions.OrderManagementCustomer);
        }
        private string GetAddressString(OrderDto context)
        {
            return $"{context.AddressDto.City}, {context.AddressDto.Street} {context.AddressDto.Building} {(!context.AddressDto.Block.IsNullOrEmpty() ? context.AddressDto.Block : string.Empty)} {(!context.AddressDto.Comment.IsNullOrEmpty() ? $"\n{context.AddressDto.Comment}" : string.Empty)}";
        }
        private async Task GetOrdersAsync(OrderState orderState)
        {
            var result = await OrderAppService.GetListAsync(
                new GetOrderListDto
                {
                    MaxResultCount = PageSize,
                    SkipCount = CurrentPage * PageSize,
                    Sorting = CurrentSorting,
                    SupplierId = IsSupplier ? CurrentUser.Id : null,
                    OrderState = orderState,
                    CustomerId = IsCustomer ? CurrentUser.Id : null,
                }
            );

            OrderList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<OrderDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.None)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page - 1;

            await GetOrdersAsync(OrderState.Submitted);

            await InvokeAsync(StateHasChanged);
        }

        private async Task UpdateOrderStatusAsync(OrderDto Order, OrderState state)
        {
            await OrderAppService.UpdateAsync(Order.Id, new CreateUpdateOrderDto
            {
                Id = Order.Id,
                UserId = Order.UserId,
                OrderState = state,
                AddressId = Order.AddressDto.Id
            });
            await GetOrdersAsync(OrderState.Submitted);
            await InvokeAsync(StateHasChanged);
        }
    }
}
