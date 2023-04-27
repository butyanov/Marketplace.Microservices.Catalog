using AutoMapper;
using Products.Api.Dto;
using Products.Api.Dto.RequestDto;
using Products.Api.Dto.RequestDto.Brand;
using Products.Api.Dto.RequestDto.Category;
using Products.Api.Dto.RequestDto.Product;
using Products.Api.Dto.RequestDto.ProductProperties;
using Products.Api.Dto.ResponseDto;
using Products.Api.Models;

namespace Products.Api.TypesMappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateProductRequest, Product>();
        CreateMap<UpdateProductRequest, Product>();
        CreateMap<CreateBrandRequest, Brand>();
        CreateMap<UpdateBrandRequest, Brand>();
        CreateMap<CreateCategoryRequest, Category>();
        CreateMap<UpdateCategoryRequest, Category>();
        CreateMap<ProductPropertiesRequest, List<ProductProperty>>();
        CreateMap<Product, ProductResponse>();
    }
}