using AutoMapper;
using DinnerWebApp.Data.Models;
using DinnerWebApp.Models;

namespace DinnerWebApp
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            ApplyMappings();
        }

        private void ApplyMappings()
        {
            CreateMap<Dinner, DinnerDao>().ReverseMap();
            CreateMap<Owner, OwnerDao>().ReverseMap();
        }
    }
}
