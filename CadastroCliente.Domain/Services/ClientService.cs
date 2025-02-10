using CadastroCliente.Domain.Interfaces;
using CadastroCliente.Domain.Models;

namespace CadastroCliente.Domain.Services
{
    public class ClientService : IClientService
    {
        private readonly IRepository<Client> _clientRepository;

        public ClientService(IRepository<Client> clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Client> CreateClient(Client client)
        {
            var clientExisting = await _clientRepository.GetAsync(x => x.Email == client.Email);

            if (clientExisting != null)
                throw new Exception("E-mail ja cadastrado!");

            return await _clientRepository.InsertAsync(client);
        }

        public async Task DeleteClient(int id)
        {
            await _clientRepository.DeleteAsync(id);
        }

        public async Task<List<Client>> GetAllClients()
        {
            var clients = await _clientRepository.GetAllWithIncludeAsync(c => c.Adressess);

            return clients.ToList();
        }

        public async Task<Client> GetClientById(int id)
        {
            return await _clientRepository.GetByIdWithIncludeAsync(c => c.Id == id, c => c.Adressess);
        }

        public async Task UpdateClient(Client client)
        {
            await _clientRepository.UpdateAsync(client);
        }
    }
}
