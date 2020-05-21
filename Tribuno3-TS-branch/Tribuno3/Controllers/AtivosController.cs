using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tribuno3.Controllers;
using Tribuno3.Camadas.DTO;
using Tribuno3.Camadas.BLL;
using System.Security.Claims;
using Tribuno3.Models;


namespace Tribuno3.Controllers
{
    public class AtivosController : Controller
    {
        RendimentoDTO DTO = new RendimentoDTO();
        RendimentoBLL BLL = new RendimentoBLL();
        UsuarioBLL LogarBLL = new UsuarioBLL();
        
       
        [Authorize]
        public ActionResult Index()
        {                 
            return View();      
        }

        [HttpPost]
        public ActionResult Inserir_Rendimento(OperacaoModel operacaoModel)
        {
            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();
                Response.StatusCode = 400;
                                
                ViewBag.ErroCadastro = string.Join(Environment.NewLine, erros); 

                //Adicionar uma modal para tratamento de erro;
               
            }

            DTO.Id_Usuario = LogarBLL.ConsultarUsuarioSessao();  
            DTO.NomeOperacao = operacaoModel.NomeOperacao;
            DTO.ValorParcela = operacaoModel.ValorParcela;
            DTO.QtdParcela = operacaoModel.QtdParcela;
            DTO.DataRecebimento = operacaoModel.DataOperacao;         
          
            if (string.IsNullOrEmpty(operacaoModel.Descricao))
                DTO.Descriacao = string.Empty;
            else
                DTO.Descriacao = operacaoModel.Descricao;
                //BLL.InserirRendimento(DTO);

            //Implementar mensagem de sucesso !
            return Redirect(Url.Action("Index","Ativos"));
            //return View("Index");
        
        }

    }
}