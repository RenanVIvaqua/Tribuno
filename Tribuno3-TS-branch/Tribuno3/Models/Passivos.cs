using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Tribuno3.Models
{
    public class Divida
    {
        public int Id_Usuario { get; set; }
        public int Id_Divida { get; set; }
        public string Nm_Divida { get; set; }
        public double Vl_Divida { get; set; }
        public int Qtd_Parcelas { get; set; }
        public string Descricao { get; set; }
        public DateTime Dt_Vencimento { get; set; } 
    }
   
}