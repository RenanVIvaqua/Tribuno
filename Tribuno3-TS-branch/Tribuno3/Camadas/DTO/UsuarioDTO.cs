using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tribuno3.Camadas.DTO
{
    public class UsuarioDTO
    {       
        private int? idUsuario { get; set; }
        private string nomeUsuario { get; set; }
        private string loginUsuario { get; set; }
        private string senhaUsuario { get; set; }
        private string email { get; set; }
        private DateTime dataCadastro { get; set; }

        #region Encapsulamento
        public int? Id_Usuario 
        {
            get { return idUsuario; }
            set { idUsuario = value; }        
        }
        public string NomeUsuario
        {
            get { return nomeUsuario; }
            set { nomeUsuario = value; }
        }
        public string LoginUsuario
        {
            get { return loginUsuario; }
            set { loginUsuario = value; }
        }
        public string SenhaUsuario
        {
            get { return senhaUsuario; }
            set { senhaUsuario = value; }
        }
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public DateTime DataCadastro
        {
            get { return dataCadastro; }
            set { dataCadastro = value; }
        }
        #endregion






    }

}