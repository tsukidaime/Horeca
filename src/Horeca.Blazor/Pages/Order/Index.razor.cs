﻿using Blazorise;
using Blazorise.DataGrid;
using Horeca.Blazor.Components;
using Horeca.OrderLines;
using Horeca.Orders;
using Horeca.ProductBids;
using Horeca.Utils;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Horeca.Blazor.Pages.Order
{
    public partial class Index
    {
        public int PageSize { get; } = HorecaConsts.DefaultGridMaxResultsCount;
        public int CurrentPage { get; set; } = 1;
        public string CurrentSorting { get; set; }
        public int TotalCount { get; set; }
        public OrderDto OrderDto { get; set; }
        public IReadOnlyList<OrderLineDto> OrderLineDtos { get; set; }
        [Inject]
        public IOrderLineAppService OrderLineAppService { get; set; }
        [Inject]
        public IOrderAppService OrderAppService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetOrderAsync();
            await GetOrderLineAsync();
        }

        private async Task GetOrderLineAsync()
        {
            if (OrderDto == null)
                return;
            var result = await OrderLineAppService.GetListAsync(
                new GetOrderLineListDto
                {
                    MaxResultCount = PageSize,
                    SkipCount = CurrentPage * PageSize,
                    Sorting = CurrentSorting,
                    OrderId = OrderDto.Id
                }
            );
            
            OrderLineDtos = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        private async Task GetOrderAsync()
        {
            OrderDto = await OrderAppService.GetOrderByUserId((Guid)CurrentUser.Id);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<OrderLineDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.None)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page - 1;

            await GetOrderAsync();

            await GetOrderLineAsync();

            await InvokeAsync(StateHasChanged);
        }

        private async Task UpdateOrderLineAsync(SavedRowItem<OrderLineDto, Dictionary<string, object>> e)
        {
            var dto = e.Item;
            await OrderLineAppService.UpdateAsync(dto.Id, new CreateUpdateOrderLineDto
            {
                Id = dto.Id,
                Count = dto.Count,
                UnitPrice = dto.UnitPrice,
                ProductBidId = dto.ProductBidId,
                OrderId = dto.OrderId
            });
        }

        private async Task SubmitOrderAsync()
        {
            await OrderAppService.UpdateAsync(OrderDto.Id, new CreateUpdateOrderDto
            {
                Id = OrderDto.Id,
                UserId = OrderDto.UserId,
                OrderState = OrderState.Submitted
            });
            await Message.Success(L["SuccefullySubmitted"]);
            await InvokeAsync(StateHasChanged);
        }
        private async Task DeleteOrderLineAsync(OrderLineDto Order)
        {
            var confirmMessage = L["OrderDeletionConfirmationMessage", Order.Id];
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await OrderLineAppService.DeleteAsync(Order.Id);
            await InvokeAsync(StateHasChanged);
        }
    }
}
