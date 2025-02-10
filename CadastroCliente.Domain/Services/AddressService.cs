using CadastroCliente.Domain.Interfaces;
using CadastroCliente.Domain.Models;

namespace CadastroCliente.Domain.Services
{
    public class AddressService : IAddressService
    {
        private readonly IRepository<Address> _addressRepository;

        public AddressService(IRepository<Address> addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<Address> CreateAddress(Address address)
        {
            return await _addressRepository.InsertAsync(address);
        }

        public async Task DeleteAddress(int id)
        {
            await _addressRepository.DeleteAsync(id);
        }

        public async Task<Address> GetAddressById(int id)
        {
            return await _addressRepository.GetByIdAsync(id);
        }

        public async Task<List<Address>> GetAllAddress()
        {
            var addresses = await _addressRepository.GetAllAsync();

            return addresses.ToList();
        }

        public async Task UpdateAddress(Address address)
        {
            await _addressRepository.UpdateAsync(address);
        }
    }
}
