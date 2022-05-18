using Blazorise;
using Blazorise.DataGrid;
using Horeca.OrderLines;
using Horeca.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Horeca.Blazor.Pages.Order
{
    public partial class List
    {
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }
        private OrderDto OrderDto { get; set; }
        private IReadOnlyList<OrderLineDto> OrderLineDtos { get; set; }

        private async Task GetOrderLineAsync()
        {
            var result = await OrderLineAppService.GetListAsync(
                new PagedAndSortedResultRequestDto
                {
                    MaxResultCount = PageSize,
                    SkipCount = CurrentPage * PageSize,
                    Sorting = CurrentSorting
                }
            );

            OrderLineDtos = result.Items.Where(x => x.OrderId == OrderDto.Id).ToList();
            TotalCount = (int)result.TotalCount;
        }

        private async Task GetOrderAsync()
        {
            var result = await OrderAppService.GetListAsync(
                new GetOrderListDto
                {
                    MaxResultCount = PageSize,
                    SkipCount = CurrentPage * PageSize,
                    Sorting = CurrentSorting
                }
            );

            OrderDto = result.Items.FirstOrDefault(x => x.UserId == CurrentUser.Id);
            TotalCount = (int)result.TotalCount;
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

        private async Task DeleteOrderLineAsync(OrderLineDto Order)
        {
            var confirmMessage = L["OrderDeletionConfirmationMessage", Order.Id];
            if (!await Message.Confirm(confirmMessage))
            {
                return;
            }

            await OrderLineAppService.DeleteAsync(Order.Id);
            await GetOrderLineAsync();
            await InvokeAsync(StateHasChanged);

        }
    }
}
