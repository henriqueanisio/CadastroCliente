
using Newtonsoft.Json;

namespace CadastroCliente.Domain.Models
{
    public class Address : Entity
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Neighborhood { get; set; }
        public string? Number { get; set; }
        public string? Complement { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}