using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CadastroCliente.Web.ViewModels
{
    public class CreateAddressViewModel : BaseViewModel
    {
        public int ClientId { get; set; }

        [DisplayName("Rua")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Street { get; set; }

        [DisplayName("Cidade")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string City { get; set; }

        [DisplayName("Estado")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string State { get; set; }

        [DisplayName("Bairro")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Neighborhood { get; set; }

        [DisplayName("Numero")]
        public string? Number { get; set; }

        [DisplayName("Complemento")]
        public string? Complement { get; set; }
    }
}
