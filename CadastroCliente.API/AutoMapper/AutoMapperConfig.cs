using AutoMapper;
using CadastroCliente.API.DTOs;
using CadastroCliente.Domain.Models;

namespace CadastroCliente.API.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Client, ClientContentDTO>().ReverseMap();
            CreateMap<Address, AddressContentDTO>().ReverseMap();

            CreateMap<Client, ClientResponseDTO>().ReverseMap();
            CreateMap<Address, AddressResponseDTO>().ReverseMap();
        }
    }
}
