using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tribuno3.Camadas.DTO;
using Tribuno3.Camadas.BLL;
using Tribuno3.Camadas.DAL;
using Tribuno3.Models;

namespace Tribuno3.Controllers
{
    public class CadastroController : Controller
    {
        UsuarioBLL BLL = new UsuarioBLL();
        UsuarioDTO DTO = new UsuarioDTO();
        AcessoDados acesso = new AcessoDados();       

        private string MsgCriticaErro 
        {
            get
            {
                var critica  = Session["MsgCriticaErro"];

                if (critica != null && critica.GetType() == typeof(string))
                    return (string)critica;
                else
                    return string.Empty;
            }
            set
            {
                Session["MsgCriticaErro"] = value;
            }
        }
        public ActionResult Index()
        {           
            ViewBag.ErroCadastro = MsgCriticaErro;
            
            return PartialView();
        }
        [HttpPost]
        public ActionResult Cadastro(UsuarioCadastro usuarioCadastro)
        {
            Session["Evento_Cadastro"] = null;
                     
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();
                Response.StatusCode = 400;

                MsgCriticaErro = string.Join(Environment.NewLine, erros);                 
                return RedirectToAction("Index", "Cadastro");
            }

            UsuarioBLL usuarioBLL = new UsuarioBLL();

            if (!usuarioBLL.ValidarLoginUsuarioExistente(usuarioCadastro.LoginUsuario))
            {
                BLL.Inserir(usuarioCadastro);
                MsgCriticaErro = "Casdastro criado com sucesso, Seja Bem Vindo !";
                return RedirectToAction("Index", "Cadastro");
            }
            else
            {
                MsgCriticaErro = "Esse Login já existe, tente outro.";
                return RedirectToAction("Index", "Cadastro");
            }
        }

        public ActionResult Logar()
        {
            ViewBag.ErroCadastro = string.Empty;
            return RedirectToAction("Index", "Login");
        }
    }
}