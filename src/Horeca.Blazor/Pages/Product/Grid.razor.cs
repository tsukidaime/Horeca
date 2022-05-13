using Horeca.Products;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Horeca.Utils;
using Horeca.Blazor.Components;
using Horeca.Blazor.State;

namespace Horeca.Blazor.Pages.Product
{
    public partial class Grid
    {
        private IReadOnlyList<ProductDto> ProductList { get; set; } = new List<ProductDto>();
        [Inject]
        public IProductAppService ProductAppService { get; set; }
        [Inject]
        public ProductGridState ProductGridState { get; set; }
        public string SearchText { get; set; }
        public int PageSize { get; } = ProductConsts.DefaultGridMaxResultsCount;
        public int CurrentPage { get; set; } = 1;
        public string CurrentSorting { get; set; }
        public PaginationData PaginationData { get; set; } = new PaginationData();
        protected override async Task OnInitializedAsync()
        {
            await GetProductsAsync();
        }
        private async Task SelectedPage(int page)
        {
            CurrentPage = page;
            await GetProductsAsync();
        }

        private async Task GetProductsAsync()
        {
            var result = await ProductAppService.GetListAsync(
                new GetProductListDto
                {
                    MaxResultCount = PageSize,
                    SkipCount = (CurrentPage-1) * PageSize,
                    Sorting = CurrentSorting,
                    OnlyApproved = true,
                    Name = SearchText,
                    CategoryId = ProductGridState.CategoryId
                }
            );

            ProductList = result.Items;
            PaginationData = new PaginationData
            {
                PageSize = PageSize,
                CurrentPage = CurrentPage,
                TotalCount = result.TotalCount,
                TotalPages = (int)Math.Ceiling(result.TotalCount / (double)PageSize),
            };
        }

        public async Task SearchAsync()
        {
            await GetProductsAsync();
            await InvokeAsync(StateHasChanged);
        }
    }
}
