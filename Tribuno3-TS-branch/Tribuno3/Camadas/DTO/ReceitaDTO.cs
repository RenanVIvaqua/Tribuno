using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tribuno3.Camadas.DTO
{
    public class ReceitaDTO
    {
        public int Id_Usuario { get; set; }
        public double? Rendimento { get; set; }
        public double? Despesa { get; set; }
        public double? Receita { get; set; }
        public double? Lucro { get; set; }
        public string Mes_ref { get; set; }
    }
}