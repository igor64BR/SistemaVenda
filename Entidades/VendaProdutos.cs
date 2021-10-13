using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Entidades
{
    public class VendaProdutos
    // Esta classe lista a quantidade de produtos comprados no total, fugindo de unidades e indo para grupo de itens.
    // Por isso que a venda possui uma coleção de VendaProdutos (quantidade de um produto), cada objeto VendaProdutos podendo apontar para um item diferente, tendo por exemplo os objetos VendaProdutos1, VendaProdutos2, VendaProdutos3 etc.
    {
        public int CodigoVenda { get; set; }
        public int CodigoProduto { get; set; }

        public float Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }

        public Produto Produto { get; set; }
        public Venda Venda { get; set; }
    }
}
