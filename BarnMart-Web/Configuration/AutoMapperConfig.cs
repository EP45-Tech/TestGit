using AutoMapper;
using BarnMart_Web.Models;
using System.Runtime.InteropServices;
using The_Barn_Mart;
namespace BarnMart_Web.Configuration

{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Seller, SellerVM>().ReverseMap();
            CreateMap<Buyer, BuyerVM>().ReverseMap();
            CreateMap<Product, ProductVM>().ReverseMap();
            CreateMap<Schedule, ScheduleVM>().ReverseMap();

        }
    }
}
