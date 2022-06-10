using Horeca.Products;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace Horeca.Blazor.Pages.Product
{
    public partial class ProductCard
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        private string GetImgPath(Guid categoryId)
        {
            return $"img/{categoryId}.jpg";
        }

        public void NavigateTo(ProductDto Product)
        {
            NavigationManager.NavigateTo($"products/{Product.Id}");
        }
    }
}
