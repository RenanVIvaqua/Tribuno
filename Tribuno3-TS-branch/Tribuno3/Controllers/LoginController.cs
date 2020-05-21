using System;
using System.Web.Mvc;
using Tribuno3.Camadas.BLL;
using Tribuno3.Camadas.DTO;
using System.Web;
using Tribuno3.Controllers;
using System.Security.Claims;
using System.Collections.Generic;
using Tribuno3.Util;
using Tribuno3.Models;
using System.Linq;

namespace Tribuno3.Controllers
{       
    public class LoginController : Controller
    {                        
        UsuarioBLL BLL = new UsuarioBLL();
        UsuarioDTO DTO = new UsuarioDTO();   
        
        public ActionResult Index()
        {
            ViewBag.Erro = Session["Erro"];
            return PartialView();
        }

        [HttpPost]
        public ActionResult Logar(UsuarioModel usuarioModel)
        { 
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();
                Response.StatusCode = 400;
                
                Session["Erro"] = string.Join(Environment.NewLine, erros);

                return RedirectToAction("Index", "Login");
            }

            DTO = BLL.ConsultarUsuario(usuarioModel.LoginUsuario,usuarioModel.SenhaUsuario);

            if (DTO.Id_Usuario != null)
            {              
                    var identity = new ClaimsIdentity(new[] 
                {
                    new Claim(ClaimTypes.Name, DTO.NomeUsuario),              
                    new Claim("Login", DTO.LoginUsuario),    
                    new Claim("ID", DTO.Id_Usuario.ToString())
                },"ApplicationCookie");

                Request.GetOwinContext().Authentication.SignIn(identity);

                Session["Id_Usuario"] = DTO.Id_Usuario.ToString();
                return RedirectToAction("Index", "Principal");
            }
            else
            {  // Se usuario e senha estiver incorreto, exibe mensagem de erro !
                Session["Erro"] = "Acesso não autorizado";
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Cadastrar()
        {
            Session["Erro"] = string.Empty;
            return RedirectToAction("Index", "Cadastro");
        }

    }


   


}