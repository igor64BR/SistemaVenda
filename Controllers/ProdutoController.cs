using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            List<Produto> produto = Db.Produto.Include(x => x.Categoria).ToList();
            Db.Dispose();
            return View(produto);
        }

        private IEnumerable<SelectListItem> GetCategoriaList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(
                new SelectListItem()
                {
                    Value = string.Empty,
                    Text = string.Empty
                }
                );

            var categorias = Db.Categoria.ToList();
            foreach (var item in categorias)
            {
                list.Add(new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Descricao.ToString()
                }
                    );
            }
            return list;
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            var viewModel = new ProdutoViewModel();
            viewModel.ListaCategorias = GetCategoriaList();


            if (id != null)
            {
                var produto = Db.Produto.Where(x => x.Codigo == id).FirstOrDefault();
                viewModel.Codigo = produto.Codigo;
                viewModel.Descricao = produto.Descricao;
                viewModel.Quantidade = produto.Quantidade;
                viewModel.Valor = produto.Valor;
                viewModel.CodigoCategoria = produto.CodigoCategoria;

            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(ProdutoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var categoria = Db.Categoria.Where(x => x.Codigo == viewModel.CodigoCategoria).FirstOrDefault();
                var objProduto = new Produto
                {
                    Codigo = viewModel.Codigo,
                    Descricao = viewModel.Descricao,
                    Quantidade = viewModel.Quantidade,
                    Valor = (decimal)viewModel.Valor,
                    CodigoCategoria = (int)viewModel.CodigoCategoria // (int) foi adicionado para automaticamente adicionar 0 ao código, caso este seja nulo. Pois o código de viewModel é anulável, enquanto o de produto não é
                };

                if (objProduto.Codigo != null)
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
                viewModel.ListaCategorias = GetCategoriaList();
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
