using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tribuno3.Camadas.DTO;
using Tribuno3.Camadas.DAL;
using System.Data;
using System.Dynamic;
using Tribuno3.Processos;
using Tribuno3.Camadas.BLL;


namespace Tribuno3.Camadas.BLL
{ 
    public class Util
    {       
        private DataTable dataTable;
        AcessoDados Acesso = new AcessoDados();

        public List<DataRow> Consultar(int User_Logado, string Procedure)
        {            
            List<System.Data.SqlClient.SqlParameter> Lista = new List<System.Data.SqlClient.SqlParameter>();

            Lista.Add(new System.Data.SqlClient.SqlParameter("ID_USUARIO", User_Logado));

            dataTable = Acesso.Consultar(Procedure, Lista);
                     
            List<DataRow> list = dataTable.AsEnumerable().ToList();
            
            return list;

        }

        public List<DataRow> ConsultarProcedure(string Procedure, Dictionary<string, string> pParametros)
        {
            dataTable = Acesso.Consultar(Procedure, ParametroSql(pParametros));

            List<DataRow> retorno = dataTable.AsEnumerable().ToList();

            return retorno;
        }

        public List<System.Data.SqlClient.SqlParameter> ParametroSql(Dictionary<string,string> pLista)
        {
            List<System.Data.SqlClient.SqlParameter> ParamLista = new List<System.Data.SqlClient.SqlParameter>();

            foreach (var x in pLista)
            {
                ParamLista.Add(new System.Data.SqlClient.SqlParameter(x.Key,x.Value));            
            }
            return ParamLista;

        }

        public List<string> RetornarRangeDeMeses(DateTime pDataInicial, DateTime pDataFinal) 
        {
            List<string> listaMeses = new List<string>();
            
            while (pDataInicial <= pDataFinal)
            {
                listaMeses.Add(System.Globalization.DateTimeFormatInfo.CurrentInfo.GetMonthName(pDataInicial.Month).ToLower());
                pDataInicial = pDataInicial.AddMonths(1);
            }
                       
            return listaMeses;
        }

    }
          

       

    
}