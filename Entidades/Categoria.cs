using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Entidades
{
    public class Categoria
    {
        [Key]
        public int? Codigo { get; set; }
        public string Descricao { get; set; }

        // Uma categoria de produtos armazena uma coleção de itens dessa categoria
        // Por isso, devemos especificar essa coleção ao EF (Coleção Produtos que contém objetos Produto)
        public ICollection<Produto> Produtos { get; set; }
    }
}
