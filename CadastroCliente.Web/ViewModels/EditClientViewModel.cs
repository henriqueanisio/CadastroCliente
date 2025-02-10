using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CadastroCliente.Web.ViewModels
{
    public class EditClientViewModel : BaseViewModel
    {
        [DisplayName("Cliente")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Name { get; set; }

        [DisplayName("E-mail")]
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        public string Email { get; set; }

        [JsonIgnore]
        [DisplayName("Alterar logo")]
        public IFormFile? LogoFile { get; set; }

        [DisplayName("Logo")]
        public string? Logo { get; set; }
    }
}
