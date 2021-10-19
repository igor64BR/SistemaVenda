using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Models
{
    public class ProdutoViewModel
    {
        // Nome das propriedades devem ser identicos aos da tabel
        public int? Codigo { get; set; }

        [Required(ErrorMessage = "Insira a descrição para o produto")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Insira a quandidade de peso em estoque")]
        public double Quantidade { get; set; }

        [Required(ErrorMessage = "Insira o valor unitário do produto")]
        public decimal? Valor { get; set; }

        [Required(ErrorMessage = "Informe a categoria do produto")]
        public int? CodigoCategoria { get; set; }

        public IEnumerable<SelectListItem> ListaCategorias { get; set; }
    }
}
