using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tribuno3.Camadas.DTO;
using Tribuno3.Processos;

namespace Tribuno3.Camadas.DAL
{
    public class ResumoFinanceiroDAL
    {
        #region Propriedades
        private AcessoDados Acesso = new AcessoDados();
        private BLL.Util Generico = new BLL.Util();
        private Dictionary<string, string> pParam = new Dictionary<string, string>();
        #endregion

        public ReceitaDTO ConsultarResumoFinanceiro(int pIdUsuario, int pMesReferente)
        {
            ReceitaDTO receita = new ReceitaDTO();
            List<System.Data.SqlClient.SqlParameter> Parametro = new List<System.Data.SqlClient.SqlParameter>();
            Parametro.Add(new System.Data.SqlClient.SqlParameter("ID_USUARIO", pIdUsuario.ToString()));
            Parametro.Add(new System.Data.SqlClient.SqlParameter("MES", pMesReferente.ToString()));

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

    }
}