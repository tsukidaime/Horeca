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
        [Parameter]
        public Guid ProductId { get; set; }

        [Parameter]
        public Guid? ProductBid { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; }
        private string CurrentSorting { get; set; }
        private int TotalCount { get; set; }
        private bool CanCreateOrder { get; set; }


        private ProductDto Product { get; set; } = new ProductDto();
        private ProductBidDto ProductBidDto { get; set; } = new ProductBidDto();
        private IReadOnlyList<ProductBidDto> ProductBidList { get; set; } = new List<ProductBidDto>();

        private OrderDto OrderDto { get; set; } = null; 
        private CreateUpdateOrderDto Order { get; set; } = new CreateUpdateOrderDto();
        private CreateUpdateOrderLineDto OrderLine { get; set; } = new CreateUpdateOrderLineDto();

        protected override async Task OnInitializedAsync()
        {
            await GetProductsAsync();
            await GetBidsAsync();
            await GetBidAsync();
            await SetPermissionsAsync();
        }

        private async Task GetProductsAsync()
        {
            Product = await ProductAppService.GetAsync(ProductId);
            await InvokeAsync(StateHasChanged);
        }

        private async Task GetBidAsync()
        {
            if(ProductBid == null)
                ProductBidDto = ProductBidList.FirstOrDefault(x => x.ProductId == ProductId);
            else
                ProductBidDto = ProductBidList.FirstOrDefault(x => x.Id == (Guid)ProductBid);

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
            await InvokeAsync(StateHasChanged);

        }

        private async Task AddToCard()
        {
            if (!CurrentUser.IsAuthenticated)
            Order = new CreateUpdateOrderDto();
            Order.UserId = (Guid)CurrentUser.Id;
            if(OrderDto == null)
                OrderDto = await OrderAppService.CreateAsync(Order);

            OrderLine.OrderId = OrderDto.Id;
            OrderLine.ProductId = Product.Id;
            OrderLine.UnitPrice = ProductBidDto.Price;
            await OrderLineAppService.CreateAsync(OrderLine);

            await InvokeAsync(StateHasChanged);
        }

        public async Task NavigateTo(ProductBidDto ProductBid)
        {
            NavigationManager.NavigateTo($"products/{Product.Id}/{ProductBid.Id}");
            await OnInitializedAsync();

        }
        private async Task SetPermissionsAsync()
        {
            CanCreateOrder = await AuthorizationService
                .IsGrantedAsync(HorecaPermissions.AddressCreate);
            
        }
    }
}
