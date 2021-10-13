﻿using Microsoft.EntityFrameworkCore;
using SistemaVenda.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Produto> Produto { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Venda> Venda { get; set; }
        public DbSet<VendaProdutos> VendaProdutos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Identificando as multiplas chaves para o VendaProdutos
            modelBuilder.Entity<VendaProdutos>()
                .HasKey(x => new { x.CodigoVenda, x.CodigoProduto });


            // VendaProdutos pertence a apenas uma venda (HasOne Venda). Esta possui diversos VendaProdutos (WithMany Produtos)*. Por último, a classe CodigoVenda é uma FK de VendaProdutos
            // *Produtos, em Venda se refere à quantidade de produtos dentro da venda, indicado pela classe VendaProdutos
            modelBuilder.Entity<VendaProdutos>()
                .HasOne(x => x.Venda)
                .WithMany(y => y.Produtos)
                .HasForeignKey(x => x.CodigoVenda);
        }
    }
}
