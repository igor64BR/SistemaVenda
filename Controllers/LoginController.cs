using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SistemaVenda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaVenda.DAL;
using SistemaVenda.Helpers;

namespace SistemaVenda.Controllers
{
    public class LoginController : Controller
    {
        protected ApplicationDbContext Db;

        public LoginController(ApplicationDbContext db)
        {
            Db = db;
        }


        // GET: LoginController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(LoginViewModel viewModel)
        {
            ViewData["ErroLogin"] = string.Empty;
            if (ModelState.IsValid)
            {
                var senha = Criptografia.GetMd5Hash(viewModel.Senha).ToUpper();
                var user = Db.Usuario.Where(x => x.Email == viewModel.Email && x.Senha == senha).FirstOrDefault();

                if (user == null)
                {
                    ViewData["ErroLogin"] = "Email ou senha não cadastrados";
                    return View(viewModel);
                }
                else
                {
                    return RedirectToAction(actionName: "Index", controllerName: "Home");
                }
            }
            else
            {
                return View(viewModel);
            }
        }
    }
}
