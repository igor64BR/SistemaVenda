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
    public class ClienteController : Controller
    {

        protected ApplicationDbContext Db;

        public ClienteController(ApplicationDbContext db)
        {
            Db = db;
        }

        public IActionResult Index()
        {
            var Clientes = Db.Cliente.ToList();
            Db.Dispose();
            return View(Clientes);
        }
        
        [HttpGet]
        public IActionResult Cadastro(int? id)
        {
            var viewModel = new ClienteViewModel();

            if (id != null)
            {
                var cliente = Db.Cliente.Where(x => x.Codigo == id).FirstOrDefault();
                viewModel.Codigo = cliente.Codigo;
                viewModel.Nome = cliente.Nome;
                viewModel.CNPJ_CPF= cliente.CNPJ_CPF;
                viewModel.Email= cliente.Email;
                viewModel.Celular = cliente.Celular;

            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Cadastro(ClienteViewModel ViewModel)
        {
            if(ModelState.IsValid)
            {
                var objCliente = new Cliente
                {
                    Codigo = ViewModel.Codigo,
                    Nome = ViewModel.Nome,
                    Celular = ViewModel.Celular,
                    CNPJ_CPF = ViewModel.CNPJ_CPF,
                    Email = ViewModel.Email,
                };

                if(objCliente.Codigo != null)
                {
                    Db.Entry(objCliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                }
                else
                {
                    Db.Cliente.Add(objCliente);
                }
                Db.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(ViewModel);
            }
        }

        public IActionResult Excluir(int? id)
        {
            var objToDelete = Db.Cliente.Where(x => x.Codigo == id).FirstOrDefault();

            Db.Cliente.Remove(objToDelete);
            Db.SaveChanges();

            return View();
        }
    }
}
