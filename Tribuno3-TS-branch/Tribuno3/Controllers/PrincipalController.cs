using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using Tribuno3.Camadas.BLL;
using Tribuno3.Camadas.DTO;
using Tribuno3.Models;
using DotNet.Highcharts.Enums;
using System.Data;

namespace Tribuno3.Controllers
{    
    public class PrincipalController : Controller
    {
        #region Propriedades           
        UsuarioBLL usuarioBLL = new UsuarioBLL();
        public OperacaoBLL operacaoBLL = new OperacaoBLL();
        public Camadas.BLL.Util utilBLL = new Camadas.BLL.Util();      
        #endregion

        [Authorize]
        public ActionResult Index()
        {           
            return View();                         
        }        

        public ActionResult Logout()
        {
            Request.GetOwinContext().Authentication.SignOut("ApplicationCookie");

            return RedirectToAction("Index", "Login");
        }

        #region Grafico  
        public EixoX GerarEixoXGrafico(List<OperacaoDTO> pListaOperacao, TipoOperacao pTipoOperacao,Color pCor, string pNome, DateTime pDataIncial, DateTime pDataFinal)
        {           
            var ValoresX = new EixoX() {Cor = pCor, Nome = pNome};
                       
            List<Object> listaEixoX = new List<object>();           
           
            while (pDataIncial <= pDataFinal)
            {
                List<double> listaCalculo = new List<double>();
                foreach (var item in pListaOperacao)
                {
                    var valor = item.Parcelas.Where(parcela => parcela.DataVencimentoParcela.Month == pDataIncial.Month && item.TipoOperacao == pTipoOperacao).Sum(parcela => parcela.Valor_Parcela);
                    listaCalculo.Add(valor);
                }

                listaEixoX.Add(listaCalculo.Sum());
                ValoresX.Valores = listaEixoX;

                pDataIncial = pDataIncial.AddMonths(1);                
            }          
            return ValoresX;
        }
      
        public ActionResult _Grafico()
        {                        
            var operacaoesRendimento = operacaoBLL.ListarOperacao(usuarioBLL.ConsultarUsuarioSessao(), TipoOperacao.Rendimento);

            var operacaoesPassivo = operacaoBLL.ListarOperacao(usuarioBLL.ConsultarUsuarioSessao(), TipoOperacao.Passivo);

            var dataIncial = DateTime.Today.AddMonths(Processos.Configuracao.Configuracao_Grafico_Meses_Anterior);

            var dataFinal  = DateTime.Today.AddMonths(Processos.Configuracao.Configuracao_Grafico_Meses_Posterior);
            
            var listaMeses = utilBLL.RetornarRangeDeMeses(dataIncial,dataFinal);

            List<List<EixoX>> valoresEixoX = new List<List<EixoX>>();          
            List<EixoX>listaEixoX = new  List<EixoX>();     
            
            listaEixoX.Add(GerarEixoXGrafico(operacaoesRendimento, TipoOperacao.Rendimento, System.Drawing.ColorTranslator.FromHtml("#57c5db"), "Rendimento", dataIncial, dataFinal) );
            listaEixoX.Add(GerarEixoXGrafico(operacaoesPassivo, TipoOperacao.Passivo, System.Drawing.ColorTranslator.FromHtml("#FF5046"), "Despesa",dataIncial, dataFinal));
    
            valoresEixoX.Add(listaEixoX);

            var grafico = new GraficoDeAreaDTO()
            {
                EixoX = valoresEixoX, Cor = Color.White,CorBorda = Color.White, Titulo = string.Empty,EixoY = listaMeses, TipoDeGrafico = ChartTypes.Areaspline
            };

            var retorno = new GraficoBLL().GerarGraficoDeAreas(grafico);
                       
            return PartialView(retorno);
                        
        }     
      
        public ActionResult _Informativos(byte alterarMes = 0)
        {        
            if(alterarMes == 1)
                Session["Mes_Referente"] = Convert.ToInt32(Session["Mes_Referente"]) - 1;

            else if (alterarMes == 2)
                Session["Mes_Referente"] = Convert.ToInt32(Session["Mes_Referente"]) + 1;
            else
                Session["Mes_Referente"] = 0;
            ViewData.Model = new ResumoFinanceiroBLL().ConsultarResumoFinanceiro(usuarioBLL.ConsultarUsuarioSessao(), Convert.ToInt32(Session["Mes_Referente"]));        
            return PartialView(ViewData.Model);
        }
        #endregion

        [HttpPost]
        public JsonResult listarDivida(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {         
            List<OperacaoDTO> lista = operacaoBLL.ListarOperacao(usuarioBLL.ConsultarUsuarioSessao(), TipoOperacao.Passivo);
            int qtd = lista.Count;
           
            OrdenarGrid(ref lista, jtStartIndex, jtPageSize, jtSorting);

            return Json(new { Result = "OK", Records = lista, TotalRecordCount = qtd });            
        }
             
        [HttpPost]
        public JsonResult listarRendimento(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            List<OperacaoDTO> lista = operacaoBLL.ListarOperacao(usuarioBLL.ConsultarUsuarioSessao(), TipoOperacao.Rendimento);
            
            int qtd = lista.Count;
            
            OrdenarGrid(ref lista, jtStartIndex,jtPageSize, jtSorting);

            return Json(new { Result = "OK", Records = lista, TotalRecordCount = qtd });

        }

        private List<OperacaoDTO> OrdenarGrid(ref List<OperacaoDTO> pLista, int pjJStartIndex, int pJtPageSize, string pJtSorting)
        {
            if (pJtPageSize != 0 && pjJStartIndex == 0)
                pLista = pLista.Take(pJtPageSize).ToList();
            else
                pLista = pLista.Skip(pjJStartIndex).ToList();

            if (!string.IsNullOrEmpty(pJtSorting))
            {
                string[] ordernacao = pJtSorting.Split(new char[] { ' ' });

                if (ordernacao.Contains("ASC"))
                {
                    if (ordernacao.Contains("NomeOperacao"))
                        pLista = pLista.OrderBy(x => x.NomeOperacao).ToList();

                    else if (ordernacao.Contains("ValorOperacao"))
                        pLista = pLista.OrderBy(x => x.ValorOperacao).ToList();

                    else if (ordernacao.Contains("ValorParcela"))
                        pLista = pLista.OrderBy(x => x.ValorParcela).ToList();

                    else if (ordernacao.Contains("QtdParcela"))
                        pLista = pLista.OrderBy(x => x.QtdParcela).ToList();

                    else if (ordernacao.Contains("Descricao"))
                        pLista = pLista.OrderBy(x => x.Descricao).ToList();
                }
                else
                {
                    if (ordernacao.Contains("NomeOperacao"))
                        pLista = pLista.OrderByDescending(x => x.NomeOperacao).ToList();

                    else if (ordernacao.Contains("ValorOperacao"))
                        pLista = pLista.OrderByDescending(x => x.ValorOperacao).ToList();

                    else if (ordernacao.Contains("ValorParcela"))
                        pLista = pLista.OrderByDescending(x => x.ValorParcela).ToList();

                    else if (ordernacao.Contains("QtdParcela"))
                        pLista = pLista.OrderByDescending(x => x.QtdParcela).ToList();

                    else if (ordernacao.Contains("Descricao"))
                        pLista = pLista.OrderByDescending(x => x.Descricao).ToList();
                }
            }

            return pLista;
        }
             
        public ActionResult ConsultarOperacao(OperacaolModal pOperacao)
        {          
          var consultaOperacao = operacaoBLL.ConsultarOperacao(usuarioBLL.ConsultarUsuarioSessao(), Convert.ToInt32(pOperacao.IdOperacao));

          return Json(new { Result = consultaOperacao });                            
        }          

        public ActionResult FiltrarParcelasGrid(GridModel pGrid)
        {
            return Json(new { Result = "OK" });
        }

        #region PartialView
        public ActionResult _Grd_Rendimento()
        {         
            return PartialView();
        }

        public ActionResult _RendimentoModal()
        {
            return PartialView();
        }

        public ActionResult _OperacaoModal()
        {
            return PartialView();
        }

        public ActionResult _Grd_Divida()
        {
            return PartialView();
        }
        #endregion

    }



}