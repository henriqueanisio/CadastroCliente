using CadastroCliente.Domain.Models;

namespace CadastroCliente.Domain.Interfaces
{
    public interface IClientService
    {
        Task<List<Client>> GetAllClients();
        Task<Client> GetClientById(int id);
        Task<Client> CreateClient(Client client);
        Task UpdateClient(Client client);
        Task DeleteClient(int id);

    }
}
