using Horeca.Blob;
using Horeca.Categories;
using Horeca.ProductBids;
using Horeca.ProductPictures;
using Horeca.Products;
using Horeca.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
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
        private List<SaveBlobInputDto> blobs = new List<SaveBlobInputDto>();
        [Inject]
        public IFileAppService FileAppService { get; set; }
        [Inject]
        public ICategoryAppService CategoryAppService { get; set; }
        [Inject]
        public IProductBidAppService ProductBidAppService { get; set; }
        [Inject]
        public IProductAppService ProductAppService { get; set; }
        [Inject]
        public IProductPictureAppService ProductPictureAppService { get; set; }
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

        private async Task OnChange(InputFileChangeEventArgs e)
        {
            blobs.Clear();
            var files = e.GetMultipleFiles(); // get the files selected by the users
            foreach (var file in files)
            {
                var resizedFile = await file.RequestImageFileAsync(file.ContentType, 640, 480); // resize the image file
                var buf = new byte[resizedFile.Size]; // allocate a buffer to fill with the file's data

                using (var stream = resizedFile.OpenReadStream())
                {
                    await stream.ReadAsync(buf); // copy the stream to the buffer
                }
                blobs.Add(new SaveBlobInputDto { Content = buf, Name = file.Name }); // convert to a base64 string!!
            }
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
                await SaveBlobs(product.Id);
            }

            await ProductBidAppService.CreateAsync(ProductBidDto);
            await Message.Success(L["SuccefullySubmitted"]);
            NavigationManager.NavigateTo("/product/management");
        }

        private async Task SaveBlobs(Guid productId)
        {
            foreach (var item in blobs)
            {
                var blobId = HashGenerator.Hash(item.Name, productId.ToString());
                item.Name = blobId;
                try
                {
                    await FileAppService.SaveBlobAsync(item);
                    await ProductPictureAppService.Create(new CreateProductPictureDto { 
                        BlobHash = blobId, 
                        ProductId = productId 
                    });
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                }
            }
        }
    }

}
