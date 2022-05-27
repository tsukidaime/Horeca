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
        private bool CanCreateOrder { get; set; }
        private bool CanEditOrder { get; set; }
        private bool CanDeleteOrder { get; set; }
        private bool CanAcceptDeclineOrder { get; set; }
        private OrderState SelectedOrderStateFilter { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await SetPermissionsAsync();
            await GetOrdersAsync();
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateOrder = await AuthorizationService
                .IsGrantedAsync(HorecaPermissions.OrderManagementCreate);
            CanEditOrder = await AuthorizationService
                .IsGrantedAsync(HorecaPermissions.OrderManagementEdit);
            CanDeleteOrder = await AuthorizationService
                .IsGrantedAsync(HorecaPermissions.OrderManagementDelete);
            CanAcceptDeclineOrder = await AuthorizationService
                .IsGrantedAsync(HorecaPermissions.OrderManagementAccept);
        }

        private async Task GetOrdersAsync()
        {
            var result = await OrderAppService.GetListAsync(
                new GetOrderListDto
                {
                    MaxResultCount = PageSize,
                    SkipCount = CurrentPage * PageSize,
                    Sorting = CurrentSorting,
                    SupplierId = CurrentUser.Id,
                    OrderState = OrderState.Submitted
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

            await GetOrdersAsync();

            await InvokeAsync(StateHasChanged);
        }
        private bool OnOrderStateCustomFilter(object itemValue, object searchValue)
        {
            if (searchValue is OrderState filter)
            {
                return filter == (OrderState)itemValue;
            }

            return true;
        }

        private async Task UpdateOrderStatusAsync(OrderDto Order, OrderState state)
        {
            await OrderAppService.UpdateAsync(Order.Id, new CreateUpdateOrderDto
            {
                Id = Order.Id,
                UserId = Order.UserId,
                OrderState = state,
            });
            await GetOrdersAsync();
            await InvokeAsync(StateHasChanged);
        }
    }
}
