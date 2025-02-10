using System.ComponentModel;

namespace CadastroCliente.Web.ViewModels
{
    public class IndexClientViewModel : BaseViewModel
    {
        [DisplayName("Cliente")]
        public string Name { get; set; }

        [DisplayName("E-mail")]
        public string Email { get; set; }

        [DisplayName("Logo")]
        public string Logo { get; set; }

        [DisplayName("Endereços")]
        public List<IndexAddressViewModel> Adressess { get; set; }
    }
}
