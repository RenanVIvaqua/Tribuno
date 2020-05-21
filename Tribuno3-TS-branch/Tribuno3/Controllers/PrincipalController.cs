using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using Tribuno3.Camadas.BLL;
using Tribuno3.Camadas.DTO;
using Tribuno3.Models;
using Tribuno3.Controllers;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System.Security.Claims;
using Tribuno3.Processos;


using System.Data;

namespace Tribuno3.Controllers
{    
    public class PrincipalController : Controller
    {      
        PrincipalBLL PrincipalBll = new PrincipalBLL();           
        Grafico MGrafico = new Grafico();       
        UsuarioBLL usuarioBLL = new UsuarioBLL();     
        
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

        #region Grid e Grafico     
      
        public ActionResult _Grafico()
        {
            #region Rendimento
            List<object> Data_Rendimento = new List<object>();
            var Lista_Rendimento = MGrafico.GetRendimento(usuarioBLL.ConsultarUsuarioSessao());
            foreach (var x in Lista_Rendimento)
            {
                Data_Rendimento.Add(Convert.ToDouble(x.ItemArray[0]));
            }          

            #endregion

            #region Lista de Divida
            List<object> Data_Divida = new List<object>();
            var Lista_Divida = MGrafico.GetDivida(usuarioBLL.ConsultarUsuarioSessao());
            foreach (var x in Lista_Divida)
            {
                Data_Divida.Add(Convert.ToDouble(x.ItemArray[0]));
            }           
            #endregion

            #region Lista de Rendimento
            List<string> Data_Meses = new List<string>();
            var Lista_Meses = MGrafico.GetMeses(usuarioBLL.ConsultarUsuarioSessao());
            foreach (var x in Lista_Meses)
            {
                Data_Meses.Add(x.ItemArray[0].ToString());
            }
            #endregion

            #region Gerar Grafico

            Highcharts columnChart = new Highcharts("areasplinechart");
            columnChart.InitChart(new Chart()
            {
                Type = DotNet.Highcharts.Enums.ChartTypes.Areaspline,
                BackgroundColor = new BackColorOrGradient(System.Drawing.Color.AliceBlue),
                Style = "fontWeight: 'bold', fontSize: '17px'",
                BorderColor = System.Drawing.Color.LightBlue,
                BorderRadius = 0,
                BorderWidth = 2             
            }) ;
            columnChart.SetTitle(new Title()
            {
                Text = "Rendimento x Despesa"

            });

            columnChart.SetSubtitle(new Subtitle()
            {
                // Text = "Comparativo de Ativo e Passivo"
            });

            columnChart.SetXAxis(new XAxis()
            {
                Type = AxisTypes.Category,
                Title = new XAxisTitle() { Text = "Anos", Style = "fontWeight: 'bold', fontSize: '14px'" },              
                Categories = Data_Meses.ToArray(),                

            });
            columnChart.SetYAxis(new YAxis()
            {
                Title = new YAxisTitle()
                {
                    Text = "R$",
                    Style = "fontWeight: 'bold', fontSize: '12px'"
                },
                ShowFirstLabel = true,
                ShowLastLabel = true,
                Min = 0
            });

            columnChart.SetCredits(new Credits()
            {
                Enabled = false
            });

            columnChart.SetLegend(new Legend
            {
                Enabled = true,
                BorderColor = System.Drawing.Color.CornflowerBlue,
                BorderRadius = 6,
                BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFADD8E6"))
            });
            
            var A = new DotNet.Highcharts.Helpers.Data(Data_Rendimento.ToArray());
            var B = new DotNet.Highcharts.Helpers.Data(Data_Divida.ToArray());

            columnChart.SetSeries(new Series[]
            {              
                 new Series()
                 {
                    Name = "Rendimento",
                    Data = A,
                    Color = System.Drawing.ColorTranslator.FromHtml("#57c5db")
                },
                new Series()
                {
                    Name = "Despesa",
                    Data = B,
                    Color = System.Drawing.ColorTranslator.FromHtml("#cc7f7f")
                },
            }
            ) ; 
            return PartialView(columnChart);
            #endregion            
        }     
      
        public ActionResult _Informativos(byte alterarMes = 0)
        {
            if(alterarMes == 1)
                Session["Mes_Referente"] = Convert.ToInt32(Session["Mes_Referente"]) - 1;

            else if (alterarMes == 2)
                Session["Mes_Referente"] = Convert.ToInt32(Session["Mes_Referente"]) + 1;
            else
                Session["Mes_Referente"] = 0;
            ViewData.Model = PrincipalBll.ReceitaObj(usuarioBLL.ConsultarUsuarioSessao(), Convert.ToInt32(Session["Mes_Referente"]));        
            return PartialView(ViewData.Model);
        }
        #endregion

        [HttpPost]
        public JsonResult listarDivida(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            PassivosBLL passivos = new PassivosBLL();

            var lista = passivos.ListarPassivo(usuarioBLL.ConsultarUsuarioSessao());
            int qtd = lista.Count;

            if (jtPageSize != 0 && jtStartIndex == 0)
                lista = lista.Take(jtPageSize).ToList();
            else
            {
                lista = lista.Skip(jtStartIndex).ToList();
            }


            if (!string.IsNullOrEmpty(jtSorting))
            {
                string []ordernacao = jtSorting.Split(new char[] {' '});

                if (ordernacao.Contains("ASC"))
                {
                    if (ordernacao.Contains("NomeOperacao"))
                        lista = lista.OrderBy(x => x.NomeOperacao).ToList();

                    else if (ordernacao.Contains("ValorOperacao"))
                        lista = lista.OrderBy(x => x.ValorOperacao).ToList();
                    
                    else if (ordernacao.Contains("ValorParcela"))
                        lista = lista.OrderBy(x => x.ValorParcela).ToList();
                    
                    else if (ordernacao.Contains("QtdParcela"))
                        lista = lista.OrderBy(x => x.QtdParcela).ToList();
                    
                    else if (ordernacao.Contains("Descricao"))
                        lista = lista.OrderBy(x => x.Descriacao).ToList();                    
                }
                else
                {
                    if (ordernacao.Contains("NomeOperacao"))
                        lista = lista.OrderByDescending(x => x.NomeOperacao).ToList();

                    else if (ordernacao.Contains("ValorOperacao"))
                        lista = lista.OrderByDescending(x => x.ValorOperacao).ToList();
                    
                    else if (ordernacao.Contains("ValorParcela"))
                        lista = lista.OrderByDescending(x => x.ValorParcela).ToList();
                    
                    else if (ordernacao.Contains("QtdParcela"))
                        lista = lista.OrderByDescending(x => x.QtdParcela).ToList();
                    
                    else if (ordernacao.Contains("Descricao"))
                        lista = lista.OrderByDescending(x => x.Descriacao).ToList();
                }
            }
            return Json(new { Result = "OK", Records = lista, TotalRecordCount = qtd });
            
        }     
        public ActionResult AlterarPassivo(OperacaoModel pPassivo)
        {        
            PassivosBLL bllPassivo = new PassivosBLL();
            PassivosDTO objPassivo = new PassivosDTO();

            objPassivo = bllPassivo.ConvertModeltoObj(pPassivo);
            if (objPassivo.Descriacao == null)
                objPassivo.Descriacao = string.Empty;

            bllPassivo.AlterarPassivo(objPassivo);

            return Json(new { Result = "OK" });
        }
        public ActionResult ConsultarPassivo(long pIdPassivo)
        {
            PassivosBLL divida = new PassivosBLL();

            var objPassivo = divida.ConsultarDivida(usuarioBLL.ConsultarUsuarioSessao(), Convert.ToInt32(pIdPassivo));

            return Json(new { Result = objPassivo });
        }
        public ActionResult _Grd_Divida()
        {           
            return PartialView();
        }
        public ActionResult _PassivoModal()
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult listarRendimento(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            RendimentoBLL rendimentoBll = new RendimentoBLL();

            var lista = rendimentoBll.ListarRendimento(usuarioBLL.ConsultarUsuarioSessao());
            int qtd = lista.Count;

            if (jtPageSize != 0 && jtStartIndex == 0)
                lista = lista.Take(jtPageSize).ToList();
            else
            {
                lista = lista.Skip(jtStartIndex).ToList();
            }


            if (!string.IsNullOrEmpty(jtSorting))
            {
                string[] ordernacao = jtSorting.Split(new char[] { ' ' });

                if (ordernacao.Contains("ASC"))
                {
                    if (ordernacao.Contains("NomeOperacao"))
                        lista = lista.OrderBy(x => x.NomeOperacao).ToList();

                    else if (ordernacao.Contains("ValorOperacao"))
                        lista = lista.OrderBy(x => x.ValorOperacao).ToList();

                    else if (ordernacao.Contains("ValorParcela"))
                        lista = lista.OrderBy(x => x.ValorParcela).ToList();

                    else if (ordernacao.Contains("QtdParcela"))
                        lista = lista.OrderBy(x => x.QtdParcela).ToList();

                    else if (ordernacao.Contains("Descricao"))
                        lista = lista.OrderBy(x => x.Descriacao).ToList();
                }
                else
                {
                    if (ordernacao.Contains("NomeOperacao"))
                        lista = lista.OrderByDescending(x => x.NomeOperacao).ToList();

                    else if (ordernacao.Contains("ValorOperacao"))
                        lista = lista.OrderByDescending(x => x.ValorOperacao).ToList();

                    else if (ordernacao.Contains("ValorParcela"))
                        lista = lista.OrderByDescending(x => x.ValorParcela).ToList();

                    else if (ordernacao.Contains("QtdParcela"))
                        lista = lista.OrderByDescending(x => x.QtdParcela).ToList();

                    else if (ordernacao.Contains("Descricao"))
                        lista = lista.OrderByDescending(x => x.Descriacao).ToList();
                }
            }
            return Json(new { Result = "OK", Records = lista, TotalRecordCount = qtd });

        }
        public ActionResult ConsultarRendimento(long pIdRendimento)
        {
            RendimentoBLL rendimentoBll = new RendimentoBLL();

            var lista = rendimentoBll.ConsultarRendimento(usuarioBLL.ConsultarUsuarioSessao(), Convert.ToInt32(pIdRendimento));

            return Json(new { Result = lista });
        }
        public ActionResult AlterarRendimento(OperacaoModel pRendimento)
        {
            RendimentoBLL rendimentoBll = new RendimentoBLL();
            RendimentoDTO objRendimento = new RendimentoDTO();

            objRendimento = rendimentoBll.ConvertModeltoObj(pRendimento);
            if (objRendimento.Descriacao == null)
                objRendimento.Descriacao = string.Empty;

            rendimentoBll.AlterarRendimento(objRendimento);

            return Json(new { Result = "OK" });
        }
        public ActionResult _Grd_Rendimento()
        {         
            return PartialView();
        }
        public ActionResult _RendimentoModal()
        {
            return PartialView();
        }

    } 



}