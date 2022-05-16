using Horeca.Products;
using Microsoft.AspNetCore.Components;

namespace Horeca.Blazor.Pages.Product
{
    public partial class ProductCard
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }


        public void NavigateTo(ProductDto Product)
        {
            NavigationManager.NavigateTo($"products/{Product.Id}");
        }
    }
}
