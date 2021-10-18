using Microsoft.AspNetCore.Mvc;
using SistemaVenda.DAL;
using SistemaVenda.Entidades;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Controllers
{
    public class ProdutoController : Controller
    {

        protected ApplicationDbContext Db;

        public ProdutoController(ApplicationDbContext db)
        {
            Db = db;
        }

        public IActionResult Index()
        {
            var produto = Db.Produto.ToList();
            Db.Dispose();
            return View(produto);
        }
        
        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            var viewModel = new ProdutoViewModel();

            if (id != null)
            {
                var produto = Db.Produto.Where(x => x.Codigo == id).FirstOrDefault();
                viewModel.Codigo = produto.Codigo;
                viewModel.Descricao = produto.Descricao;
                viewModel.Quantidade = produto.Quantidade;
                viewModel.Valor = produto.Valor;
                viewModel.CodigoCategoria= produto.CodigoCategoria;

            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(ProdutoViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var categoria = Db.Categoria.Where(x => x.Codigo == viewModel.CodigoCategoria).FirstOrDefault();
                var objProduto = new Produto
                {
                    Codigo = viewModel.Codigo,
                    Descricao = viewModel.Descricao,
                    Quantidade = viewModel.Quantidade,
                    Valor = viewModel.Valor,
                    CodigoCategoria = (int)viewModel.Codigo // (int) foi adicionado para automaticamente adicionar 0 ao código, caso este seja nulo. Pois o código de viewModel é anulável, enquanto o de produto não é
                };

                if(objProduto.Codigo != null)
                {
                    Db.Entry(objProduto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                else
                {
                    Db.Produto.Add(objProduto);
                }
                Db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(viewModel);
            }
        }

        public IActionResult Excluir(int? id)
        {
            var objToDelete = Db.Produto.Where(x => x.Codigo == id).FirstOrDefault();

            Db.Produto.Remove(objToDelete);
            Db.SaveChanges();

            return View();
        }
    }
}
