using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Insira um email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Insira a senha")]
        public string Senha { get; set; }
    }
}
