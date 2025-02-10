using CadastroCliente.Domain.Models;

namespace CadastroCliente.Domain.Interfaces
{
    public interface IAuthKeyRepository
    {
        Task<AuthKey> GetByCompanyNameAndKey(string companyName, string key);
    }
}
