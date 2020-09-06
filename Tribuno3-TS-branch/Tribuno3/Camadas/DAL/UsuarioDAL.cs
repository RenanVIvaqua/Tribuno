using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tribuno3.Camadas.DAL;
using Tribuno3.Camadas.DTO;
using Tribuno3.Camadas.BLL;
using Tribuno3.Util;
using Tribuno3.Models;

namespace Tribuno3.Camadas.DAL
{
    public class UsuarioDAL
    {
        #region Atributos
        protected AcessoDados Acesso = new AcessoDados();
        protected DTO.UsuarioDTO Usuario = new UsuarioDTO();
        protected BLL.Util Generico = new BLL.Util(); 
        #endregion

        /// <summary>
        /// Método para preencher objeto Usuario
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="pSenha"></param>
        /// <returns></returns>
        public DTO.UsuarioDTO ConsultarUsuario(string pUsuario, string pSenha)
        {
            Dictionary<string, string> pParam = new Dictionary<string, string>();

            pParam.Add("LoginUsuario", pUsuario);
            pParam.Add("SenhaUsuario", Hash.GerarHasg(pSenha));        

            var ds = Acesso.Consultar(Processos.Executar.Consultar_Usuario, Generico.ParametroSql(pParam));

            if (ds.Rows.Count > 0)
            {
                if (ds.Rows[0].ItemArray[0] != DBNull.Value)
                    Usuario.Id_Usuario = Convert.ToInt32(ds.Rows[0].ItemArray[0]);

                if (ds.Rows[0].ItemArray[1] != DBNull.Value)
                    Usuario.NomeUsuario = Convert.ToString(ds.Rows[0].ItemArray[1]);

                if (ds.Rows[0].ItemArray[2] != DBNull.Value)
                    Usuario.LoginUsuario = Convert.ToString(ds.Rows[0].ItemArray[2]);

                if (ds.Rows[0].ItemArray[3] != DBNull.Value)
                    Usuario.Email = Convert.ToString(ds.Rows[0].ItemArray[3]);

                if (ds.Rows[0].ItemArray[4] != DBNull.Value)
                    Usuario.DataCadastro = Convert.ToDateTime(ds.Rows[0].ItemArray[4]);
            }

            return Usuario;
        }

        /// <summary>
        /// Método para preencher objeto Usuario pelo ID
        /// </summary>
        /// <param name="pIdUsuario"></param>
        /// <returns></returns>
        public DTO.UsuarioDTO ConsultarUsuario(string pIdUsuario)
        {
            Dictionary<string, string> pParam = new Dictionary<string, string>();

            pParam.Add("ID_USUARIO", pIdUsuario);           

            var ds = Acesso.Consultar(Processos.Executar.Consultar_Usuario_Id, Generico.ParametroSql(pParam));

            if (ds.Rows.Count > 0)
            {
                if (ds.Rows[0].ItemArray[0] != DBNull.Value)
                    Usuario.Id_Usuario = Convert.ToInt32(ds.Rows[0].ItemArray[0]);

                if (ds.Rows[0].ItemArray[1] != DBNull.Value)
                    Usuario.NomeUsuario = Convert.ToString(ds.Rows[0].ItemArray[1]);

                if (ds.Rows[0].ItemArray[2] != DBNull.Value)
                    Usuario.LoginUsuario = Convert.ToString(ds.Rows[0].ItemArray[2]);

                if (ds.Rows[0].ItemArray[3] != DBNull.Value)
                    Usuario.Email = Convert.ToString(ds.Rows[0].ItemArray[3]);

                if (ds.Rows[0].ItemArray[4] != DBNull.Value)
                    Usuario.DataCadastro = Convert.ToDateTime(ds.Rows[0].ItemArray[4]);
            }

            return Usuario;
        }
        
        /// <summary>
        /// Método Para Incluir Novo Usuário
        /// </summary>
        /// <param name="pUsuario"></param>
        public void InsirirUsuario(UsuarioCadastro pUsuario)
        {
            Dictionary<string, string> pParam = new Dictionary<string, string>();

            pParam.Add("NM_USUARIO", pUsuario.NomeUsuario);
            pParam.Add("NM_LOGIN", pUsuario.LoginUsuario);
            pParam.Add("NM_SENHA", Hash.GerarHasg(pUsuario.SenhaUsuario));
            // pParam.Add("EMAIL", pUsuario.Email); Não Implementado Email na Tela de Cadastro

            Acesso.Executar(Processos.Executar.Inserir_Usuario, Generico.ParametroSql(pParam));

        }

        public bool ConsultarLoginbase(string pNomeLogin)
        {
            Dictionary<string, string> pParam = new Dictionary<string, string>();

            pParam.Add("NomeLogin", pNomeLogin);    
            var retorno = Acesso.Consultar(Processos.Executar.Consultar_NomeLogin, Generico.ParametroSql(pParam));

            if (retorno.Rows.Count == 0)
                return false;
            else
                return true;

        }
        


    }
}