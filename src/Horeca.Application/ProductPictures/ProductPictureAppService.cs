using Horeca.Blob;
using Horeca.Models;
using Horeca.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Horeca.ProductPictures
{
    public class ProductPictureAppService :
        IProductPictureAppService
    {
        private readonly IRepository<ProductPicture> _repository;
        private readonly IFileAppService _fileAppService;
        public ProductPictureAppService(IRepository<ProductPicture> repository, IFileAppService fileAppService)
        {
            _repository = repository;
            _fileAppService = fileAppService;   
        }

        public async Task Create(CreateProductPictureDto dto)
        {
            await _repository.InsertAsync(new ProductPicture
            {
                BlobHash = dto.BlobHash,
                ProductId = dto.ProductId
            });
        }

        public async Task<List<ProductPictureDto>> GetPicturesByProductId(Guid productId)
        {
            var pictures =  await _repository.GetListAsync(x => x.ProductId == productId);
            var dtos = new List<ProductPictureDto>();
            foreach (var picture in pictures)
            {
                var blob = await _fileAppService.GetBlobAsync(new GetBlobRequestDto { Name = picture.BlobHash });
                var imagesrc = Convert.ToBase64String(blob.Content);
                var imageDataURL = string.Format("data:image/jpeg;base64,{0}", imagesrc);
                dtos.Add(new ProductPictureDto { BlobHash = blob.Name, ImageUrl = imageDataURL});
            }
            return dtos;
        }
    }
}
