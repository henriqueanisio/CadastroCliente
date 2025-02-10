using CadastroCliente.Domain.Interfaces;
using CadastroCliente.Domain.Models;

namespace CadastroCliente.Data.Repositories
{
    public class AuthKeyRepository : IAuthKeyRepository
    {
        private readonly IRepository<AuthKey> _authKeyRepository;

        public AuthKeyRepository(IRepository<AuthKey> authKeyRepository)
        {
            _authKeyRepository = authKeyRepository;
        }

        public async Task<AuthKey> GetByCompanyNameAndKey(string companyName, string key)
        {
            var authKey = await _authKeyRepository.GetAsync(x => x.CompanyName == companyName && x.Key == key);

            return authKey;
        }
    }
}
