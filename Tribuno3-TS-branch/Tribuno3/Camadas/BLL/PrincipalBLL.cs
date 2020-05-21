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
    public class PrincipalBLL
    {       
        private DataTable DT;
        AcessoDados Acesso = new AcessoDados();

        public List<DataRow> Consultar1(int User_Logado, string Procedure)
        {            
            List<System.Data.SqlClient.SqlParameter> Lista = new List<System.Data.SqlClient.SqlParameter>();

            Lista.Add(new System.Data.SqlClient.SqlParameter("ID_USUARIO", User_Logado));

            DT = Acesso.Consultar(Procedure, Lista);
                     
            List<DataRow> list = DT.AsEnumerable().ToList();
            
            return list;

        }
            
        public ReceitaDTO ReceitaObj(int id_Usuario, int Mes)
        {
            ReceitaDTO receita = new ReceitaDTO();
            List<System.Data.SqlClient.SqlParameter> Parametro = new List<System.Data.SqlClient.SqlParameter>();
            Parametro.Add(new System.Data.SqlClient.SqlParameter("ID_USUARIO", id_Usuario.ToString()));
            Parametro.Add(new System.Data.SqlClient.SqlParameter("MES", Mes.ToString()));

            var ds = Acesso.Consultar(Executar.Consultar_Resumo_Financeiro_Mes, Parametro);

            if (ds.Rows[0].ItemArray[0] != DBNull.Value)
                receita.Id_Usuario = Convert.ToInt32(ds.Rows[0].ItemArray[0]);
            if (ds.Rows[0].ItemArray[1] != DBNull.Value)
                receita.Rendimento = Convert.ToDouble(ds.Rows[0].ItemArray[1]);
            if (ds.Rows[0].ItemArray[2] != DBNull.Value)
                receita.Despesa = Convert.ToDouble(ds.Rows[0].ItemArray[2]);
            if (ds.Rows[0].ItemArray[3] != DBNull.Value)
                receita.Receita = Convert.ToDouble(ds.Rows[0].ItemArray[3]);
            if (ds.Rows[0].ItemArray[4] != DBNull.Value)
                receita.Lucro = Convert.ToDouble(ds.Rows[0].ItemArray[4]);
            if (ds.Rows[0].ItemArray[5] != DBNull.Value)
                receita.Mes_ref = Convert.ToString(ds.Rows[0].ItemArray[5]);

            return receita;

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

    }
          

       

    
}