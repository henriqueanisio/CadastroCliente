namespace CadastroCliente.Domain.Interfaces
{
    public interface IAuthKeyService
    {
        Task<string?> Authenticate(string companyName, string apiKey);
    }
}
