using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CadastroCliente.Web.ViewModels
{
    public class CreateClientViewModel : BaseViewModel
    {
        [DisplayName("Cliente")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Name { get; set; }

        [DisplayName("E-mail")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Email { get; set; }

        [JsonIgnore]
        [DisplayName("Logo")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public IFormFile LogoFile { get; set; }

        public string? Logo { get; set; }

        [JsonIgnore]
        [DisplayName("Rua")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Street { get; set; }

        [JsonIgnore]
        [DisplayName("Cidade")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string City { get; set; }

        [JsonIgnore]
        [DisplayName("Estado")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string State { get; set; }

        [JsonIgnore]
        [DisplayName("Bairro")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Neighborhood { get; set; }

        [JsonIgnore]
        [DisplayName("Numero")]
        public string? Number { get; set; }

        [JsonIgnore]
        [DisplayName("Complemento")]
        public string? Complement { get; set; }


        public List<IndexAddressViewModel>? Adressess { get; set; }
    }
}
