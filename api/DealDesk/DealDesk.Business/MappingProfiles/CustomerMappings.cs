using AutoMapper;
using DealDesk.Business.Dtos;
using DealDesk.DataAccess.Entities;

namespace DealDesk.Business.MappingProfiles
{
    public class CustomerMappings : Profile
    {
        public CustomerMappings()
        {
            CreateMap<CustomerRequest, Customer>();
            CreateMap<Customer, CustomerResponse>();
        }
    }
}
