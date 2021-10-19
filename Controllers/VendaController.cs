using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SistemaVenda.DAL;
using SistemaVenda.Entidades;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVenda.Controllers
{
    public class VendaController : Controller
    {
        protected ApplicationDbContext Db;

        public VendaController(ApplicationDbContext db)
        {
            Db = db;
        }

        public IActionResult Index()
        {
            List<Venda> vendas = Db.Venda.ToList();
            Db.Dispose();
            return View(vendas);
        }

        private IEnumerable<SelectListItem> GetProdutoList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(
                new SelectListItem()
                {
                    Value = string.Empty,
                    Text = string.Empty
                }
                );

            var produtos = Db.Produto.ToList();
            foreach (var item in produtos)
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

        private IEnumerable<SelectListItem> GetClientesList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(
                new SelectListItem()
                {
                    Value = string.Empty,
                    Text = string.Empty
                }
                );

            var clientes = Db.Cliente.ToList();
            foreach (var item in clientes)
            {
                list.Add(new SelectListItem()
                {
                    Value = item.Codigo.ToString(),
                    Text = item.Nome.ToString()
                }
                    );
            }
            return list;
        }

        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            var viewModel = new VendaViewModel();
            viewModel.ListaClientes = GetClientesList();
            viewModel.ListaProdutos = GetProdutoList();


            if (id != null)
            {
                var cliente = Db.Venda.Where(x => x.Codigo == id).FirstOrDefault();
                viewModel.Codigo = cliente.Codigo;
                viewModel.Data = cliente.Data;
                viewModel.CodigoCliente = cliente.CodigoCliente;
                viewModel.Total = cliente.Total;
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(VendaViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //var categoria = Db.Categoria.Where(x => x.Codigo == viewModel.CodigoCategoria).FirstOrDefault();
                var objVenda = new Venda()
                {
                    Codigo = viewModel.Codigo,
                    Data = (DateTime)viewModel.Data,
                    CodigoCliente = (int)viewModel.CodigoCliente,
                    Total = viewModel.Total,
                    Produtos = JsonConvert.DeserializeObject<ICollection<VendaProdutos>>(viewModel.JsonProdutos)
                };

                if (objVenda.Codigo != null)
                {
                    Db.Entry(objVenda).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                else
                {
                    Db.Venda.Add(objVenda);
                }
                Db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                viewModel.ListaProdutos = GetProdutoList();
                viewModel.ListaClientes = GetClientesList();
                return View(viewModel);
            }
        }

        public IActionResult Excluir(int? id)
        {
            var objToDelete = Db.Venda.Where(x => x.Codigo == id).FirstOrDefault();

            Db.Venda.Remove(objToDelete);
            Db.SaveChanges();

            return View();
        }

        [HttpGet("LerValorProduto/{CodigoProduto}")]
        public decimal LerValorProduto(int CodigoProduto)
        {
            return Db.Produto.Where(x => x.Codigo == CodigoProduto).Select(y => y.Valor).FirstOrDefault();
        }
    }
}
