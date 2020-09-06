using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Tribuno3.Camadas.DTO
{
    public class GraficoDeAreaDTO
    {                    

        public List< List<EixoX> > EixoX { get;set; }

        public List<string> EixoY { get; set; } 
        
        public string Titulo { get; set; }

        public Color Cor { get; set; }

        public Color CorBorda { get; set; }

        public ChartTypes TipoDeGrafico { get; set; }
    }

    public class EixoX 
    {
        public Color Cor { get; set; }

        public List<object> Valores { get; set; }

        public string Nome { get; set; }
    }

}