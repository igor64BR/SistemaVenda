using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Models
{
    public class VendaViewModel
    {
        // Nome das propriedades devem ser identicos aos da tabel
        public int? Codigo { get; set; }

        [Required(ErrorMessage = "Insira a data da venda")]
        public DateTime? Data { get; set; }

        [Required(ErrorMessage = "Insira o cliente")]
        public double CodigoCliente { get; set; }

        public IEnumerable<SelectListItem> ListaClientes { get; set; }
        public IEnumerable<SelectListItem> ListaProdutos { get; set; }
        public string JsonProdutos { get; set; }
        public decimal Total{ get; set; }
    }
}
