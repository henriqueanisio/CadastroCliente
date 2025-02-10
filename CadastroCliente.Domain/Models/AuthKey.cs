using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastroCliente.Domain.Models
{
    public class AuthKey : Entity
    {
        public string CompanyName { get; set; }
        public string Key { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
