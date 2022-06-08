using Horeca.Products;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Horeca.ProductPictures
{
    public interface IProductPictureAppService : IApplicationService
    {
        Task<List<ProductPictureDto>> GetPicturesByProductId(Guid productId);

        Task Create(CreateProductPictureDto dto);
    }
}
