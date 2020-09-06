using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Web;
using Tribuno3.Camadas.DTO;
namespace Tribuno3.Camadas.BLL
{
    public class GraficoBLL
    {
        private const string GraficoDeArea = "areasplinechart";

        public Highcharts GerarGraficoDeAreas(GraficoDeAreaDTO pGrafico)
        {
            Highcharts columnChart = new Highcharts("areasplinechart");
            columnChart.InitChart(new Chart()
            {
                Type = pGrafico.TipoDeGrafico,
                BackgroundColor = new BackColorOrGradient(pGrafico.Cor),
                Style = "fontWeight: 'bold', fontSize: '17px'",
                BorderColor = pGrafico.CorBorda,                
                BorderRadius = 0,
                BorderWidth = 2
            });

            columnChart.SetTitle(new Title()
            {
                Text = pGrafico.Titulo

            });            

            columnChart.SetXAxis(new XAxis()
            {
                Type = AxisTypes.Category,
                Title = new XAxisTitle() { Style = "fontWeight: 'bold', fontSize: '14px'" },
                Categories = pGrafico.EixoY.ToArray()           
            });

            columnChart.SetYAxis(new YAxis()
            {
                Title = new YAxisTitle()
                {
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

            List<Series> listaValoresX = new List<Series>();

            foreach (var item in pGrafico.EixoX)
            {
                foreach (var valoresX in item)
                {
                   var valores = new DotNet.Highcharts.Helpers.Data(valoresX.Valores.ToArray()) ;
                                                           
                   var serie = new Series() { Name = valoresX.Nome, Data = valores, Color = valoresX.Cor};

                    listaValoresX.Add(serie);                                                       
                }               
            }
            columnChart.SetSeries(listaValoresX.ToArray());
            return columnChart;
        }

    }


}