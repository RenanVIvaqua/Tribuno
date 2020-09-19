using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tribuno3.Camadas.DTO;
using Tribuno3.Camadas.BLL;
using Tribuno3.Models;
using System.Globalization;

namespace Tribuno3.Controllers
{
    public class OperacaoController : Controller
    {
        #region Propriedades

        private OperacaoBLL operacaoBLL = new OperacaoBLL();
        /// <summary>
        /// Lista de Parcelas
        /// </summary>

        private List<OperacaoParcelasDTO> ListaParcelas
        {
            get
            {
                var list = Session["ListaParcelas"];

                if (list != null && list.GetType() == typeof(List<OperacaoParcelasDTO>))
                    return (List<OperacaoParcelasDTO>)list;
                else
                    return new List<OperacaoParcelasDTO>();
            }
            set
            {
                Session["ListaParcelas"] = value;
            }
        }

        #endregion

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListarParcelas(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            List<OperacaoParcelasDTO> lista = new List<OperacaoParcelasDTO>();
            int qtdPaginacao = 0;

            qtdPaginacao = ListaParcelas.Count();

            if (jtPageSize != 0 && jtStartIndex == 0)
                lista = ListaParcelas.Take(jtPageSize).ToList();
            else
                lista = ListaParcelas.Skip(jtStartIndex).ToList();

            return Json(new { Result = "OK", Records = lista, TotalRecordCount = qtdPaginacao });
        }

        public ActionResult CalcularParcelas(OperacaoModel operacao)
        {
            if (!this.ModelState.IsValid)
                return RedirectToAction("~/Principal/_OperacaoModal", operacao);

            try
            {
                OperacaoBLL operacaoBLL = new OperacaoBLL();
                string mensagemCritica = string.Empty;

                ListaParcelas = operacaoBLL.GerarParcelas(operacao);

                double total = ListaParcelas.Sum(x => x.Valor_Parcela);
                var valorFormatado = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", total);
                mensagemCritica = "Valor total da Operação " + valorFormatado;

                var result = new { Success = "True", Message = mensagemCritica };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {
                var result = new { Success = "False", Message = erro.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GravarParcelas(OperacaoModel operacao)
        {
            if (!this.ModelState.IsValid)
                return RedirectToAction("~/Principal/_OperacaoModal", operacao);

            try
            {
                if (operacao.IdOperacao > 0)
                    operacaoBLL.DeletarOperacao(operacao.IdOperacao);

                string identity_Inserido;
                if (ListaParcelas.Count > 0)
                {
                    OperacaoDTO objOperacao = operacaoBLL.ConvertModeltoObj(operacao);

                    objOperacao.ValorOperacao = ListaParcelas.Sum(x => x.Valor_Parcela);
                    identity_Inserido = operacaoBLL.InserirOperacao(objOperacao);

                    if (int.TryParse(identity_Inserido, out int identityInserido))
                        operacaoBLL.InserirParcela(ListaParcelas, identityInserido);
                }

                var result = new { Success = "True", Message = "Operação registrada." };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {
                var result = new { Success = "False", Message = erro.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
           
        public ActionResult DeletarOperacao(OperacaolModal pOperModal)
        {
            try
            {            
                operacaoBLL.DeletarOperacao(pOperModal.IdOperacao);

                var result = new { Success = "True", Message = "Operação registrada." };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch(Exception erro)
            {
                var result = new { Success = "False", Message = erro.Message };
                return Json(result, JsonRequestBehavior.DenyGet);
            }
        }

        public ActionResult CarregarParcelas(OperacaolModal pOperacaoModal)
        {
            try
            {
                if (pOperacaoModal.Cadastro)
                    ListaParcelas = new List<OperacaoParcelasDTO>();
                else
                    ListaParcelas = new OperacaoBLL().ConsultarParcelas(pOperacaoModal.IdOperacao);

                var result = new { Success = "True"};

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            catch (Exception erro)
            {
                var result = new { Success = "False", Message = erro.Message };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

    }
}


