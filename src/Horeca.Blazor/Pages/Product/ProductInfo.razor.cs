using Horeca.ProductBids;
using Horeca.Products;
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

        private ProductDto Product { get; set; } = new ProductDto();
        private ProductBidDto ProductBidDto { get; set; } = new ProductBidDto();
        private IReadOnlyList<ProductBidDto> ProductBidList { get; set; } = new List<ProductBidDto>();


        protected override async Task OnInitializedAsync()
        {
            await GetProductsAsync();
            await GetBidsAsync();
            await GetBidAsync();
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
            await InvokeAsync(StateHasChanged);
        }

        public async Task NavigateTo(ProductBidDto ProductBid)
        {
            NavigationManager.NavigateTo($"products/{Product.Id}/{ProductBid.Id}");
            await OnInitializedAsync();

        }
    }
}
