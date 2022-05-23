using Blazorise;
using Blazorise.DataGrid;
using Horeca.OrderLines;
using Horeca.Orders;
using Horeca.Permissions;
using Horeca.ProductBids;
using Horeca.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Horeca.Blazor.Pages.Product
{
    public partial class ProductInfo
    {
        private Alert countAlert;
        [Parameter]
        public Guid ProductId { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }
        private bool CanCreateOrder { get; set; }


        private ProductDto Product { get; set; } = new ProductDto();
        private IReadOnlyList<ProductBidDto> ProductBidList { get; set; } = new List<ProductBidDto>();
        private Dictionary<Guid, int> OrderCounts { get;set; } = new Dictionary<Guid, int>();
        private OrderDto Order { get; set; }
        private CreateUpdateOrderLineDto OrderLine { get; set; } = new CreateUpdateOrderLineDto();

        protected override async Task OnInitializedAsync()
        {
            await GetOrderAsync();
            await GetProductAsync();
            await GetBidsAsync();
            await SetPermissionsAsync();
        }
        private async Task GetOrderAsync()
        {
            Order = await OrderAppService.GetOrderByUserId((Guid)CurrentUser.Id);
        }
        private async Task GetProductAsync()
        {
            Product = await ProductAppService.GetAsync(ProductId);
            await InvokeAsync(StateHasChanged);
        }

        public async Task OnDataGridReadAsync(DataGridReadDataEventArgs<ProductBidDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.None)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page - 1;

            await GetBidsAsync();

            await InvokeAsync(StateHasChanged);
        }

        private async Task GetBidsAsync()
        {
            var result = await ProductBidAppService.GetListByProductIdAsync(
                new GetProductBidListDto
                {
                    MaxResultCount = PageSize,
                    SkipCount = CurrentPage * PageSize,
                    Sorting = CurrentSorting,
                    ProductId = ProductId
                }
            );
            
            ProductBidList = result.Items;
            TotalCount = (int)result.TotalCount;
            OrderCounts.Clear();
            foreach (var bid in ProductBidList)
            {
                OrderCounts.Add(bid.Id, 0);
            }
            await InvokeAsync(StateHasChanged);

        }

        public async Task AddToBasket(ProductBidDto dto)
        {
            if (Order == null)
            {
                Order = await OrderAppService.CreateAsync(new CreateUpdateOrderDto
                {
                    UserId = (Guid)CurrentUser.Id
                });
            }

            if (OrderCounts[dto.Id] <= 0)
            {
                await countAlert.Show();
                return;
            }

            OrderLine.OrderId = Order.Id;
            OrderLine.ProductBidId = dto.Id;
            OrderLine.UnitPrice = dto.Price;
            OrderLine.Count = OrderCounts[dto.Id];
            await OrderLineAppService.CreateAsync(OrderLine);

            NavigationManager.NavigateTo("/products");
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateOrder = await AuthorizationService
                .IsGrantedAsync(HorecaPermissions.AddressCreate);
        }
    }
}
