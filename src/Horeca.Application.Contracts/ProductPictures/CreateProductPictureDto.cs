using System;
using Volo.Abp.Application.Dtos;

namespace Horeca.ProductPictures
{
    public class CreateProductPictureDto
    {
        public Guid ProductId { get; set; }
        public string BlobHash { get; set; }
    }
}
