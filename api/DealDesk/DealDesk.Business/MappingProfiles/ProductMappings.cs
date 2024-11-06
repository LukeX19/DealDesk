using AutoMapper;
using DealDesk.Business.Dtos;
using DealDesk.DataAccess.Entities;

namespace DealDesk.Business.MappingProfiles
{
    public class ProductMappings : Profile
    {
        public ProductMappings()
        {
            CreateMap<ProductRequest, Product>();
            CreateMap<Product, ProductResponse>();
        }
    }
}
