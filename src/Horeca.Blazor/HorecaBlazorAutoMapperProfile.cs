using AutoMapper;
using Horeca.Products;

namespace Horeca.Blazor;

public class HorecaBlazorAutoMapperProfile : Profile
{
    public HorecaBlazorAutoMapperProfile()
    {
        CreateMap<ProductDto, CreateUpdateProductDto>();
    }
}
