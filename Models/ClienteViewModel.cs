using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Models
{
    public class ClienteViewModel
    {
        public int? Codigo { get; set; }

        [Required(ErrorMessage = "Informe o Nome")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o CPF ou CNPJ")]
        public string CNPJ_CPF { get; set; }

        [Required(ErrorMessage = "Informe o Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe o Celular")]
        public string Celular { get; set; }
    }
}
