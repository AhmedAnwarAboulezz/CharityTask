using AutoMapper;
using Common.StandardInfrastructure;
using Orders.Data.Entities;
using Orders.Service.Dto;
using Microsoft.AspNetCore.Http;

namespace Orders.Service.AutoMapper
{
    public class OrdersProfile : Profile
    {

        public OrdersProfile()
        {
            MapCountry();            
            MapGender();           
        }

        

        private void MapCountry()
        {
            CreateMap<CountryDto, Country>().ReverseMap();
        }
      
        private void MapGender()
        {
            CreateMap<GenderDto, Gender>().ReverseMap()
                .ForMember(dest => dest.GenderNameFl, opts =>
                opts.MapFrom(src => Helper.ChangeProperty() == (int)Helper.ChangePropertyEnum.Ar ||
                Helper.ChangeProperty() == (int)Helper.ChangePropertyEnum.ArEn ? src.GenderNameSl : src.GenderNameFl))
                .ForMember(dest => dest.GenderNameSl,
                opts => opts.MapFrom(src => Helper.ChangeProperty() == (int)Helper.ChangePropertyEnum.En
                 || Helper.ChangeProperty() == (int)Helper.ChangePropertyEnum.ArEn ? src.GenderNameFl : src.GenderNameSl));
            CreateMap<DropdownDto, Gender>().ReverseMap()
                .ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))

                .ForMember(dest => dest.NameFl, opts =>
                opts.MapFrom(src => Helper.ChangeProperty() == (int)Helper.ChangePropertyEnum.Ar ||
                Helper.ChangeProperty() == (int)Helper.ChangePropertyEnum.ArEn ? src.GenderNameSl : src.GenderNameFl))
                .ForMember(dest => dest.NameSl,
                opts => opts.MapFrom(src => Helper.ChangeProperty() == (int)Helper.ChangePropertyEnum.En
                 || Helper.ChangeProperty() == (int)Helper.ChangePropertyEnum.ArEn ? src.GenderNameFl : src.GenderNameSl));

        }
      

    }
   
}
