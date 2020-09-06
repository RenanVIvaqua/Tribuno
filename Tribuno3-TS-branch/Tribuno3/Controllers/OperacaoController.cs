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

        public string CalcularParcelas(OperacaoModel operacao)
        {
            OperacaoBLL operacaoBLL = new OperacaoBLL();

            string mensagemCritica = string.Empty;

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                mensagemCritica = string.Join(Environment.NewLine, erros);

                return mensagemCritica;
            }
           
            ListaParcelas = operacaoBLL.GerarParcelas(operacao);

            double total;

            if (operacao.TipodeCalculo == TipodeCalculo.Parcela)
            {
                total = ListaParcelas.Sum(x => x.Valor_Parcela);
                var valorFormatado = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", total);
                mensagemCritica = "Valor total da Operação " + valorFormatado;
            }
            else if (operacao.TipodeCalculo == TipodeCalculo.Operacao)
            {
                total = ListaParcelas[0].Valor_Parcela;
                var valorFormatado = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", total);
                mensagemCritica = "Valor da parcela " + valorFormatado;
            }
            return mensagemCritica;
        }

        public ActionResult GravarParcelas(OperacaoModel operacao)
        {
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
                //Implementar Modal de sucesso
                return RedirectToAction(controllerName:"Principal",actionName:"Index");
            }
            catch 
            {
                //Implementar Modal de Erro
                return RedirectToAction(controllerName:"Principal", actionName:"Index");
            }

            
        }

        public void DeletarOperacao(OperacaolModal pOperModal)
        {
            operacaoBLL.DeletarOperacao(pOperModal.IdOperacao);
        }

        public void CarregarParcelas(OperacaolModal pOperacaoModal)
        {
            if(pOperacaoModal.Cadastro)
                ListaParcelas = new List<OperacaoParcelasDTO>();
            else
                ListaParcelas = new OperacaoBLL().ConsultarParcelas(pOperacaoModal.IdOperacao);
        }
       
    }
}


