using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tribuno3.Camadas.BLL;
using Tribuno3.Camadas.DTO;

namespace Tribuno3.Camadas.DAL
{
    public class AcessoDados :InterfaceBanco
    {     
        
        /// <summary>
        /// String de Conexão
        /// </summary>
        /// <returns></returns>
        protected static string StringConexao()
        {
            ConnectionStringSettings conn = System.Configuration.ConfigurationManager.ConnectionStrings["DB_Tribuno"];
            if (conn != null)
                return conn.ConnectionString;
            else
                return string.Empty;
        }

        /// <summary>
        /// Método para executar uma procedure
        /// </summary>
        /// <param name="NomeProcedure"></param>
        /// <param name="parametros"></param>
        internal override string Executar(string NomeProcedure, List<SqlParameter> parametros)
        {
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(StringConexao());

            comando.Connection = conexao;
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = NomeProcedure;
            foreach (var item in parametros)
                comando.Parameters.Add(item);

            conexao.Open();
            try
            {
                var retorno = comando.ExecuteScalar(); 
                
               if(retorno != null)
                    return retorno.ToString();
                else
                    return string.Empty;               
            }
            finally
            {
                conexao.Close();
            }
        }
        
        /// <summary>
        /// Método para Consultar o banco de dados 
        /// </summary>
        /// <param name="NomeProcedure"></param>
        /// <param name="parametros"></param>
        /// <returns></returns>
        internal override DataTable Consultar(string NomeProcedure, List<SqlParameter> parametros)
        {
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(StringConexao());

            comando.Connection = conexao;
            comando.CommandType = System.Data.CommandType.StoredProcedure;
            comando.CommandText = NomeProcedure;
            foreach (var item in parametros)
             comando.Parameters.Add(item);

            SqlDataAdapter adapter = new SqlDataAdapter(comando);
            DataTable ds = new DataTable();
            conexao.Open();

            try
            {
                adapter.Fill(ds);
            }
            finally
            {
                conexao.Close();
            }       

            return ds;
        }              

        /// <summary>
        /// Método para executar um comando SQL
        /// </summary>
        /// <param name="QuerySelect"></param>
        /// <returns></returns>
        internal override DataTable ConsultarQuery(string QuerySelect)
        {
            SqlCommand comando = new SqlCommand();
            SqlConnection conexao = new SqlConnection(StringConexao());

            comando.Connection = conexao;
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = QuerySelect;        

            SqlDataAdapter adapter = new SqlDataAdapter(comando);
            DataTable ds = new DataTable();
            conexao.Open();
            try
            {
                adapter.Fill(ds);
            }
            finally
            {
                conexao.Close();
            }

            return ds;
        }

    }
}