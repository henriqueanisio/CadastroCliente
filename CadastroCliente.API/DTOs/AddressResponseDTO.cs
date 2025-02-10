namespace CadastroCliente.API.DTOs
{
    public class AddressResponseDTO
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Neighborhood { get; set; }
        public string? Number { get; set; }
        public string? Complement { get; set; }
    }
}
