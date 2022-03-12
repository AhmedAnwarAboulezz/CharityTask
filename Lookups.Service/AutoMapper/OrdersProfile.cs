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
            MapProduct();
            MapOrder();           
        }


        private void MapProduct()
        {
            CreateMap<ProductDto, Product>().ReverseMap();
        }


      
        private void MapOrder()
        {
            CreateMap<OrderDto, Order>().ReverseMap();
            CreateMap<OrderDetailDto, OrderDetail>().ReverseMap();
        }
      

    }
   
}
