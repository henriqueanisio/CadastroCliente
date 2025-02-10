namespace CadastroCliente.Domain.Models
{
    public class Client : Entity
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public string Logo { get; set; }
        public List<Address>? Adressess { get; set; }
    }
}
