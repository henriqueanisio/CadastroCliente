using CadastroCliente.Domain.Models;

namespace CadastroCliente.API.DTOs
{
    public class ClientContentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Logo { get; set; }
        public List<AddressContentDTO>? Adressess { get; set; }
    }
}
