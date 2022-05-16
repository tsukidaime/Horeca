using Horeca.Products;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Horeca.Blazor.Pages.Product
{
    public partial class ProductInfo
    {
        [Parameter]
        public Guid ProductId { get; set; }

        private ProductDto Product { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetProductsAsync();
        }

        private async Task GetProductsAsync()
        {
            Product = await ProductAppService.GetAsync(ProductId);
            await InvokeAsync(StateHasChanged);
        }
    }
}
