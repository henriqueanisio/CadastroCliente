using CadastroCliente.Domain.Models;

namespace CadastroCliente.Domain.Interfaces
{
    public interface IAddressService
    {
        Task<List<Address>> GetAllAddress();
        Task<Address> GetAddressById(int id);
        Task<Address> CreateAddress(Address address);
        Task UpdateAddress(Address address);
        Task DeleteAddress(int id);
    }
}
