using Blazorise;
using Blazorise.DataGrid;
using Horeca.Blob;
using Horeca.OrderLines;
using Horeca.Orders;
using Horeca.Permissions;
using Horeca.ProductBids;
using Horeca.ProductPictures;
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
        private ProductDto Product { get; set; } = new ProductDto();
        private List<ProductPictureDto> ProductPictures { get; set; }  = new List<ProductPictureDto>();
        private IReadOnlyList<ProductBidDto> ProductBidList { get; set; } = new List<ProductBidDto>();
        private Dictionary<Guid, int> OrderCounts { get;set; } = new Dictionary<Guid, int>();
        private OrderDto Order { get; set; }
        private string selectedSlide;
        private CreateUpdateOrderLineDto OrderLine { get; set; } = new CreateUpdateOrderLineDto();
        [Inject]
        public IProductPictureAppService ProductPictureAppService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetOrderAsync();
            await GetProductAsync();
            await GetBidsAsync();
        }
        private async Task GetOrderAsync()
        {
            Order = await OrderAppService.GetOrderByUserId((Guid)CurrentUser.Id);
        }
        private async Task GetProductAsync()
        {
            Product = await ProductAppService.GetAsync(ProductId);
            ProductPictures = await ProductPictureAppService.GetPicturesByProductId(ProductId);

            await InvokeAsync(StateHasChanged);
        }

        private void NavigatoToSupplier(ProductBidDto dto)
        {
            NavigationManager.NavigateTo($"/supplier/{dto.UserId}");
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
            OrderLine.SupplierId = dto.UserId;
            OrderLine.OrderId = Order.Id;
            OrderLine.ProductBidId = dto.Id;
            OrderLine.UnitPrice = dto.Price;
            OrderLine.Count = OrderCounts[dto.Id];
            await OrderLineAppService.CreateAsync(OrderLine);
            await Message.Success(L["SuccesfullyAdded"]);
            NavigationManager.NavigateTo("/products");
        }

        private string GetImgPath(Guid categoryId)
        {
            return $"img/{categoryId}.jpg";
        }
    }
}
