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
    public class PassivoController : Controller
    {
        #region Propriedades

        /// <summary>
        /// Lista de Parcelas
        /// </summary>
        private List<OperacaoParcelasDTO> listaParcelasPassivo
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

        private List<OperacaoParcelasDTO> listaParcelasRendimento
        {
            get
            {
                var list = Session["ListaParcelasRendimento"];

                if (list != null && list.GetType() == typeof(List<OperacaoParcelasDTO>))
                    return (List<OperacaoParcelasDTO>)list;
                else
                    return new List<OperacaoParcelasDTO>();
            }
            set
            {
                Session["ListaParcelasRendimento"] = value;
            }
        }

        private TipoOperacao pTipoOperacaoDefinida 
        {
            get
            {
                var pTipoOperacaoDefinida = Session["pTipoOperacaoDefinida"];

                if (pTipoOperacaoDefinida != null && pTipoOperacaoDefinida.GetType() == typeof(TipoOperacao))
                    return (TipoOperacao)pTipoOperacaoDefinida;
                else
                    return new TipoOperacao();
            }
            set
            {
                Session["pTipoOperacaoDefinida"] = value;
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

            if (pTipoOperacaoDefinida != TipoOperacao.NaoDefinido)
            {
                qtdPaginacao  = pTipoOperacaoDefinida == TipoOperacao.Passivo ? listaParcelasPassivo.Count() : listaParcelasRendimento.Count();

                if (jtPageSize != 0 && jtStartIndex == 0)
                    lista = pTipoOperacaoDefinida == TipoOperacao.Passivo ? listaParcelasPassivo.Take(jtPageSize).ToList() : listaParcelasRendimento.Take(jtPageSize).ToList();
                else
                    lista = pTipoOperacaoDefinida == TipoOperacao.Passivo ? listaParcelasPassivo.Skip(jtStartIndex).ToList() : listaParcelasRendimento.Skip(jtStartIndex).ToList();
            }
                       
            return Json(new { Result = "OK", Records = lista, TotalRecordCount = qtdPaginacao });
        }
              
        public string CalcularParcelas(OperacaoModel operacao)
        {
            PassivosBLL passivo = new PassivosBLL();

            string mensagemCritica = string.Empty;

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();            

                mensagemCritica = string.Join(Environment.NewLine, erros);

                return mensagemCritica;
            }                     

            PassivosDTO objPassivo =  passivo.ConvertModeltoObj(operacao);
            
            if (pTipoOperacaoDefinida == TipoOperacao.Passivo)
                listaParcelasPassivo = passivo.GerarParcelas(objPassivo);
            else if (pTipoOperacaoDefinida == TipoOperacao.Rendimento)
                listaParcelasRendimento = passivo.GerarParcelas(objPassivo);

            double total;           

            if (operacao.TipodeCalculo == TipodeCalculo.parcela)
            {
                total = listaParcelasRendimento.Sum(x => x.Valor_Parcela);
                var valorFormatado = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", total);
                mensagemCritica = "Valor total da Operação " + valorFormatado;
            }
            else if (operacao.TipodeCalculo == TipodeCalculo.opercacao)
            {
                total = listaParcelasRendimento[0].Valor_Parcela;
                var valorFormatado = string.Format(CultureInfo.GetCultureInfo("pt-BR"), "{0:C}", total);
                mensagemCritica = "Valor da parcela " + valorFormatado;
            }

            return mensagemCritica;
        }

        public void GravarParcelas(OperacaoModel operacao) 
        {
            if (pTipoOperacaoDefinida == TipoOperacao.Passivo)
            {
                PassivosBLL passivo = new PassivosBLL();

                if (operacao.IdOperacao > 0)
                    passivo.DeletarPassivo(operacao.IdOperacao);

                string identity_Inserido;
                if (listaParcelasPassivo.Count > 0)
                {
                    identity_Inserido = passivo.InserirPassivo(passivo.ConvertModeltoObj(operacao), listaParcelasPassivo);

                    if (int.TryParse(identity_Inserido, out int identityInserido))
                        passivo.InserirParcela(listaParcelasPassivo, identityInserido);
                }
            }
            else if (pTipoOperacaoDefinida == TipoOperacao.Rendimento)
            {
                RendimentoBLL redimento = new RendimentoBLL();
                if (operacao.IdOperacao > 0)
                    redimento.DeletarRendimento(operacao.IdOperacao);

                string identity_Inserido;
                if (listaParcelasRendimento.Count > 0)
                {
                    identity_Inserido = redimento.InserirRendimento(redimento.ConvertModeltoObj(operacao), listaParcelasRendimento);

                    if (int.TryParse(identity_Inserido, out int identityInserido))
                        redimento.InserirParcela(listaParcelasRendimento, identityInserido);
                }

            }
        }

        public void DeletarOperacao(OperacaolModal pOperModal) 
        {
            if (pOperModal.TipoOperacao == TipoOperacao.Passivo)
                new PassivosBLL().DeletarPassivo(pOperModal.Id_Operacao);
            else if (pOperModal.TipoOperacao == TipoOperacao.Rendimento)
                new RendimentoBLL().DeletarRendimento(pOperModal.Id_Operacao);
        }

        public void DefinirTipoOperacao(OperacaolModal pTipoOperacao)
        {
            pTipoOperacaoDefinida = pTipoOperacao.TipoOperacao;
        }

        public void CarregarParcelas(OperacaolModal objPassivoModal)
        {
            objPassivoModal.TipoOperacao = pTipoOperacaoDefinida;
            if (!objPassivoModal.Cadastro && objPassivoModal.TipoOperacao == TipoOperacao.Passivo)
            {
                listaParcelasPassivo = new PassivosBLL().ConsultarParcelas(objPassivoModal.Id_Operacao);
            }
            else if (!objPassivoModal.Cadastro && objPassivoModal.TipoOperacao == TipoOperacao.Rendimento)
            {
                listaParcelasRendimento = new RendimentoBLL().ConsultarParcelas(objPassivoModal.Id_Operacao);
            }
            else if (objPassivoModal.Cadastro && objPassivoModal.TipoOperacao == TipoOperacao.Passivo)
            {
                listaParcelasPassivo = new List<OperacaoParcelasDTO>();
            }
            else if (objPassivoModal.Cadastro && objPassivoModal.TipoOperacao == TipoOperacao.Rendimento)
            {
                listaParcelasRendimento = new List<OperacaoParcelasDTO>();
            }

        }
    }      
}


