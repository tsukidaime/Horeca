using Horeca.Categories;
using Horeca.ProductBids;
using Horeca.Products;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace Horeca.Blazor.Pages.Product
{
    public partial class Create
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public string SelectedStep { get; set; } = "start";
        public CreateUpdateProductDto ProductDetails { get; set; } = new CreateUpdateProductDto();
        public CreateUpdateProductBidDto ProductBidDto { get; set; } = new CreateUpdateProductBidDto();
        public CategoryDto SelectedCategory { get; set; } = new CategoryDto();
        [Inject]
        public ICategoryAppService CategoryAppService { get; set; }
        [Inject]
        public IProductBidAppService ProductBidAppService { get; set; }
        [Inject]
        public IProductAppService ProductAppService { get; set; }
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
            if (IsExistingProduct)
            {
                ProductBidDto.ProductId = ProductDetails.Id;
            }
            else
            {
                var product = await ProductAppService.CreateAsync(ProductDetails);
                ProductBidDto.ProductId = product.Id;
            }

            await ProductBidAppService.CreateAsync(ProductBidDto);
            await Message.Success(L["SuccefullySubmitted"]);
            NavigationManager.NavigateTo("/product/management");
        }
    }
}
