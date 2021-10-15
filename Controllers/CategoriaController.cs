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
    public class CategoriaController : Controller
    {
        protected ApplicationDbContext Db;

        public CategoriaController(ApplicationDbContext localContext)
        {
            Db = localContext;
        }

        public IActionResult Index()
        {
            IEnumerable<Categoria> Categorias = Db.Categoria.ToList();
            Db.Dispose();

            return View(Categorias);
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            var viewModel = new CategoriaViewModel();

            if (id != null) // Signigica que há um item cadastrado com este ID
            {
                // Settando o item enviado para o formulário para que o código explicitado seja mostrado sem erros
                var item = Db.Categoria.Where(x => x.Codigo == id).FirstOrDefault();
                viewModel.Codigo = item.Codigo;
                viewModel.Descricao = item.Descricao;
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(CategoriaViewModel item)
        {
            if (ModelState.IsValid)
            {
                var objCategoria = new Categoria
                {
                    Codigo = item.Codigo,
                    Descricao = item.Descricao
                };

                if (objCategoria.Codigo != null)
                {
                    Db.Entry(objCategoria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                else
                {
                    Db.Categoria.Add(objCategoria);
                }
                Db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }
        }

        public IActionResult Excluir(int? id)
        {
            var objCategoria = Db.Categoria.Where(x => x.Codigo == id).FirstOrDefault();
            if (id != null)
            {
                Db.Categoria.Remove(objCategoria);
                Db.SaveChanges();
            }
            return View();
        }
    }
}
