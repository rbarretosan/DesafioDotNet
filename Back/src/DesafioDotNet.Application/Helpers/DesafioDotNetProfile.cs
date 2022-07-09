using AutoMapper;
using DesafioDotNet.Application.Dtos;
using DesafioDotNet.Domain;

namespace DesafioDotNet.Application.Helpers
{
    public class DesafioDotNetProfile: Profile
    {
        public DesafioDotNetProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();        
        }
    }
}