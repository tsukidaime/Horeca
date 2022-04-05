using Horeca.Categories;
using Horeca.ProductBids;
using Horeca.Products;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Horeca.Blazor.Pages.Product
{
    public partial class Create
    {
        public string SelectedStep { get; set; } = "selectStep";
        public CreateUpdateProductDto ProductDetails { get; set; } = new CreateUpdateProductDto();
        public CreateUpdateProductBidDto ProductBidDto { get; set; } = new CreateUpdateProductBidDto();
        public CategoryDto SelectedCategory { get; set; } = new CategoryDto();
        [Parameter]
        public ICategoryAppService CategoryAppService { get; set; }
        public bool IsExistingProduct { get; set; }
        private Task OnSelectedStepChanged(string name)
        {
            SelectedStep = name;
            return Task.CompletedTask;
        }

        public void NavigateTo(string step)
        {
            SelectedStep = step;
        }

        public async Task CreateProduct()
        {
            await ProductAppService.CreateAsync(ProductDetails);
        }
    }
}
