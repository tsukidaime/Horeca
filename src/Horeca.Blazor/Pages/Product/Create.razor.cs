using Horeca.Categories;
using Horeca.ProductBids;
using Horeca.Products;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.BlobStoring;

namespace Horeca.Blazor.Pages.Product
{
    public partial class Create
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public string SelectedStep { get; set; } = "start";
        public CreateUpdateProductDto ProductDetails { get; set; } = new CreateUpdateProductDto();
        public CreateUpdateProductBidDto ProductBidDto { get; set; } = new CreateUpdateProductBidDto();
        public IReadOnlyList<IBrowserFile> files { get; set; }
        public CategoryDto SelectedCategory { get; set; } = new CategoryDto();
        [Inject]
        public ICategoryAppService CategoryAppService { get; set; }
        private readonly IBlobContainer _blobContainer;
        [Inject]
        public IProductBidAppService ProductBidAppService { get; set; }
        [Inject]
        public IProductAppService ProductAppService { get; set; }
        public bool IsExistingProduct { get; set; }

        public Create()
        {
        }

        public Create(IBlobContainer blobContainer)
        {
            _blobContainer = blobContainer;
        }

        private Task OnSelectedStepChanged(string name)
        {
            SelectedStep = name;
            return Task.CompletedTask;
        }

        public void NavigateTo(string step)
        {
            SelectedStep = step;
        }

        private void OnChange(InputFileChangeEventArgs e)
        {
            files = e.GetMultipleFiles();
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
                try
                {
                    await _blobContainer.SaveAsync(product.Id.ToString(), files[0].OpenReadStream());
                }catch (Exception ex)
                {
                }
            }

            await ProductBidAppService.CreateAsync(ProductBidDto);
            await Message.Success(L["SuccefullySubmitted"]);
            NavigationManager.NavigateTo("/product/management");
        }
    }

}
