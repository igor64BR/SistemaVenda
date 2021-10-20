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
        protected IHttpContextAccessor HttpCA;

        public LoginController(ApplicationDbContext db, IHttpContextAccessor httpContext)
        {
            Db = db;
            HttpCA = httpContext;
        }


        // GET: LoginController
        public ActionResult Index(int? id)
        {
            if (id != null)
            {
                if (id == 0)
                {
                    HttpCA.HttpContext.Session.Clear();
                }
            }
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
                    HttpCA.HttpContext.Session.SetString(Secao.NOME_USUARIO, user.Nome);
                    HttpCA.HttpContext.Session.SetString(Secao.EMAIL_USUARIO, user.Email);
                    HttpCA.HttpContext.Session.SetInt32(Secao.CODIGO_USUARIO, (int)user.Codigo);
                    HttpCA.HttpContext.Session.SetInt32(Secao.LOGADO, 1);
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
