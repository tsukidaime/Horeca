using AutoMapper;
using Horeca.Categories;
using Horeca.Models;
using Horeca.Products;
using Horeca.ProductBids;
using Volo.Abp.AutoMapper;
using Horeca.Addresses;
using Horeca.Orders;
using Horeca.OrderLines;
using Horeca.ProductPictures;

namespace Horeca;

public class HorecaApplicationAutoMapperProfile : Profile
{
    public HorecaApplicationAutoMapperProfile()
    {
        CreateMap<CreateUpdateProductDto, Product>();
        CreateMap<ProductDto, CreateUpdateProductDto>();
        CreateMap<Product, ProductDto>()
            .ForMember(x => x.CategoryName, map => map.MapFrom(y => y.Category.Name));
        CreateMap<ProductBid, ProductBidDto>();
        CreateMap<CreateUpdateProductBidDto, ProductBid>();
        CreateMap<ProductBidDto, CreateUpdateProductBidDto>();
        CreateMap<CreateUpdateCategoryDto, Category>();
        CreateMap<Category, CategoryDto>()
            .ForMember(x=>x.ParentName, map=>map.MapFrom(y=>y.Parent.Name))
            .ForMember(x=>x.Children, map=>map.MapFrom(y=>y.SubCategories));
        CreateMap<CreateUpdateAddressDto, Address>();
        CreateMap<AddressDto, CreateUpdateAddressDto>();
        CreateMap<Address, AddressDto>();
        CreateMap<CreateUpdateOrderDto, Order>();
        CreateMap<OrderDto, CreateUpdateOrderDto>();
        CreateMap<Order, OrderDto>();
        CreateMap<CreateUpdateOrderLineDto, OrderLine>();
        CreateMap<OrderLineDto, CreateUpdateOrderLineDto>();
        CreateMap<OrderLine, OrderLineDto>();
        CreateMap<ProductPicture, CreateProductPictureDto>();
    }
}
