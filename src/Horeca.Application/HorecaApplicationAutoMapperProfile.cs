using AutoMapper;
using Horeca.Categories;
using Horeca.Models;
using Volo.Abp.AutoMapper;

namespace Horeca;

public class HorecaApplicationAutoMapperProfile : Profile
{
    public HorecaApplicationAutoMapperProfile()
    {
        CreateMap<CreateUpdateCategoryDto, Category>();
        CreateMap<Category, CategoryDto>()
            .ForMember(x=>x.ParentName, map=>map.MapFrom(y=>y.Parent.Name));
    }
}
