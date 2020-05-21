using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tribuno3.Camadas.BLL;
using System.Data;
using Tribuno3.Camadas.DTO;


namespace Tribuno3.Models
{
   
    public class Grafico
    {
        PrincipalBLL BLL = new PrincipalBLL();

        public List<DataRow> Rendimento { get; set; }

        public List<DataRow> Divida { get; set; }

        public List<DataRow> Meses { get; set; }
        
        public List<DataRow> GetRendimento(int USER)
        {            
            Rendimento = BLL.Consultar1(USER, "PRC_GRAFICO_RENDIMENTO");                            

            return Rendimento;
        }

        public List<DataRow> GetDivida(int USER)
        {
            Divida = BLL.Consultar1(USER, "PRC_GRAFICO_DIVIDA");
            
            return Divida;
        }

        public List<DataRow> GetMeses(int USER)
        {          
            Meses = BLL.Consultar1(USER, "PRC_Grafico_Mes");

            

            return Meses;
        }




    }
}