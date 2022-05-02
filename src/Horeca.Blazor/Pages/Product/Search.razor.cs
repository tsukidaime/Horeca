using Blazorise;
using Blazorise.DataGrid;
using Horeca.Products;
using Horeca.Utils;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace Horeca.Blazor.Pages.Product
{
    public partial class Search
    {
        [Parameter]
        public string SelectedStep { get; set; }
        [Parameter]
        public EventCallback<string> SelectedStepChanged { get; set; }
        [Parameter]
        public CreateUpdateProductDto ProductDetails { get; set; }
        [Parameter]
        public EventCallback<CreateUpdateProductDto> ProductDetailsChanged { get; set; }
        [Parameter]
        public bool IsExistingProduct { get; set; }
        [Parameter]
        public EventCallback<bool> IsExistingProductChanged { get; set; }
        public string SearchText { get; set; }
        public IReadOnlyList<ProductDto> ProductList { get; set; }
        public int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        public int CurrentPage { get; set; }
        public string CurrentSorting { get; set; }
        public int TotalCount { get; set; }
        public bool HiddenProductsGrid { get; set; }

        public async Task OnDataGridReadAsync(DataGridReadDataEventArgs<ProductDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.None)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page - 1;

            await SearchAsync();

            await InvokeAsync(StateHasChanged);
        }
        public async Task Select(ProductDto productDto)
        {
            await ProductDetailsChanged.InvokeAsync(ObjectMapper.Map<ProductDto, CreateUpdateProductDto>(productDto));
            await SelectedStepChanged.InvokeAsync(StepName.Details);
            await IsExistingProductChanged.InvokeAsync(true);
        }
        public async Task SearchAsync()
        {
            var result = await ProductAppService.GetListByNameAsync(
                new GetProductListDto
                {
                    MaxResultCount = PageSize,
                    SkipCount = CurrentPage * PageSize,
                    Sorting = CurrentSorting,
                    Filter = SearchText
                }
            );

            ProductList = result.Items;
            TotalCount = (int)result.TotalCount;
            HiddenProductsGrid = TotalCount > 0;
        }
    }
}
