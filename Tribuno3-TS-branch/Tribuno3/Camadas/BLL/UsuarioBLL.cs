using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tribuno3.Camadas.DTO;
using Tribuno3.Camadas.DAL;
using Tribuno3.Util;
using Tribuno3.Controllers;
using System.Web.Mvc;
using Tribuno3.Models;


namespace Tribuno3.Camadas.BLL
{
    
    public class UsuarioBLL 
    {
        #region Propriedades
        AcessoDados Acesso = new AcessoDados();
        UsuarioDAL usuarioDAL = new UsuarioDAL();

        public static string idUsuario
        {
            get
            {               
              return (string)System.Web.HttpContext.Current.Session["Id_Usuario"];               
            }
            set
            {
                if (IsHttpRuntime())
                {
                    System.Web.HttpContext.Current.Session["Id_Usuario"] = value;
                }
            }
        }
        private static bool IsHttpRuntime()
        {
            if (HttpRuntime.AppDomainAppId == null)
            {
                throw new ArgumentNullException(@"HttpRuntime.AppDomainAppId is null.  SessionManager can only be used in a web application");
            }
            return true;
        }
        #endregion

        /// <summary>
        /// Método para Consultar Usuário
        /// </summary>
        /// <param name="pUsuario"></param>
        /// <param name="pSenha"></param>
        /// <returns></returns>
        public UsuarioDTO ConsultarUsuario(string pUsuario, string pSenha)
        {
            return usuarioDAL.ConsultarUsuario(pUsuario, pSenha);                               
        }

        /// <summary>
        /// Método para Consultar Usuário pelo Id
        /// </summary>
        /// <param name="pIdusuario"></param>
        /// <returns></returns>
        public UsuarioDTO ConsultarUsuario(string pIdusuario)
        {
            return usuarioDAL.ConsultarUsuario(pIdusuario);
        }

        /// <summary>
        /// Método para Consultar Usuário Logado na Sessão
        /// </summary>
        /// <returns></returns>
        public int ConsultarUsuarioSessao()
        {            
            int.TryParse(idUsuario, out int idUsuarioSessao);

            return idUsuarioSessao;
        }

        public bool ValidarLoginUsuarioExistente(string pNomeLogin)
        {
            return usuarioDAL.ConsultarLoginbase(pNomeLogin);         
        }

        /// <summary>
        /// Método Para Inserir Usuário
        /// </summary>
        /// <param name="DTO"></param>
        public void Inserir(UsuarioCadastro pUsario)
        {                   
            usuarioDAL.InsirirUsuario(pUsario);          
        }

    }
}