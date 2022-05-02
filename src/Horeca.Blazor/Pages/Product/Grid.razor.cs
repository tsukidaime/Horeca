using Horeca.Products;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Horeca.Utils;
using Horeca.Blazor.Components;

namespace Horeca.Blazor.Pages.Product
{
    public partial class Grid
    {
        [Parameter]
        public Guid? CategoryId { get; set; }
        private IReadOnlyList<ProductDto> ProductList { get; set; }
        [Inject]
        public IProductAppService ProductAppService { get; set; }
        public int PageSize { get; } = ProductConsts.DefaultGridMaxResultsCount;
        public int CurrentPage { get; set; }
        public string CurrentSorting { get; set; }
        public PaginationData PaginationData { get; set; }
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
            var result = CategoryId != null ? await ProductAppService.GetListByCategoryAsync(
                CategoryId,
                new GetProductListDto
                {
                    MaxResultCount = PageSize,
                    SkipCount = CurrentPage * PageSize,
                    Sorting = CurrentSorting,
                    OnlyApproved = true
                }
            ) :
            await ProductAppService.GetListAsync(
                new GetProductListDto
                {
                    MaxResultCount = PageSize,
                    SkipCount = CurrentPage * PageSize,
                    Sorting = CurrentSorting,
                    OnlyApproved = true
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
    }
}
