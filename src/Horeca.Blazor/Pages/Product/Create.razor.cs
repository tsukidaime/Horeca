using FileActionsDemo;
using Horeca.Categories;
using Horeca.ProductBids;
using Horeca.Products;
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
        List<SaveBlobInputDto> filesBase64 = new List<SaveBlobInputDto>();
        [Inject]
        public IFileAppService _fileAppService { get; set; }
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

        private async Task OnChange(InputFileChangeEventArgs e)
        {
            var files = e.GetMultipleFiles(); // get the files selected by the users
            foreach (var file in files)
            {
                var resizedFile = await file.RequestImageFileAsync(file.ContentType, 640, 480); // resize the image file
                var buf = new byte[resizedFile.Size]; // allocate a buffer to fill with the file's data

                using (var stream = resizedFile.OpenReadStream())
                {
                    await stream.ReadAsync(buf); // copy the stream to the buffer
                }
                filesBase64.AddFirst(new SaveBlobInputDto { Content = buf, Name = file.Name }); // convert to a base64 string!!
                try
                {
                    await _fileAppService.SaveBlobAsync(filesBase64[0]);
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                }
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

                try
                {
                    await _fileAppService.SaveBlobAsync(filesBase64[0]);
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                }
            }

            await ProductBidAppService.CreateAsync(ProductBidDto);
            await Message.Success(L["SuccefullySubmitted"]);
            NavigationManager.NavigateTo("/product/management");
        }
    }

}
