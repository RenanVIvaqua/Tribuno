using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tribuno3.Camadas.DTO;
using Tribuno3.Camadas.BLL;
using Tribuno3.Camadas.DAL;
using Tribuno3.Models;
using System.Security.Claims;

namespace Tribuno3.Controllers
{
    public class CadastroController : Controller
    {
        UsuarioBLL BLL = new UsuarioBLL();
        UsuarioDTO DTO = new UsuarioDTO();
        AcessoDados acesso = new AcessoDados();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "NomeUsuario,LoginUsuario,SenhaUsuario,Email")] UsuarioCadastro usuarioCadastro)
        {          

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();
                Response.StatusCode = 400;
                                
                return View(usuarioCadastro);
            }

            UsuarioBLL usuarioBLL = new UsuarioBLL();

            if (!usuarioBLL.ValidarLoginUsuarioExistente(usuarioCadastro.LoginUsuario))
            {
                BLL.Inserir(usuarioCadastro);

                var usuarioNovo = BLL.ConsultarUsuario(usuarioCadastro.LoginUsuario, usuarioCadastro.SenhaUsuario);

                if (usuarioNovo.Id_Usuario != null)
                {
                    var identity = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, usuarioNovo.NomeUsuario),
                    new Claim("Login", usuarioNovo.LoginUsuario),
                    new Claim("ID", usuarioNovo.Id_Usuario.ToString())
                }, "ApplicationCookie");

                    Request.GetOwinContext().Authentication.SignIn(identity);

                    Session["Id_Usuario"] = usuarioNovo.Id_Usuario.ToString();
                    return RedirectToAction("Index", "Principal");
                }

                return View(usuarioCadastro);
            }
            else
            {
                ViewBag.ErroCadastro = "Esse Login já está sendo utilizado, tente outro.";
                return View(usuarioCadastro);
            }
        }

        public ActionResult Logar()
        {
            ViewBag.ErroCadastro = string.Empty;
            return RedirectToAction("Index", "Login");
        }
    }
}