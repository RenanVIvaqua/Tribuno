using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tribuno3.Camadas.DAL;
using Tribuno3.Camadas.DTO;
using Tribuno3.Models;


namespace Tribuno3.Camadas.BLL
{
    /// <summary>
    /// Camada de negócio Passivo
    /// </summary>
    public class PassivosBLL
    {
        #region Propriedades
        protected AcessoDados Acesso = new AcessoDados();
        protected PrincipalBLL Generico = new PrincipalBLL();
        protected DTO.PassivosDTO divida = new PassivosDTO();
        protected PassivosDAL PassivoDAL = new PassivosDAL();

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

        #region Métodos

        /// <summary>
        /// Método para Inserir um Passivo no Banco de Dados
        /// </summary>
        /// <param name="DTO"></param>        
        public string InserirPassivo(PassivosDTO pPassivo, List<OperacaoParcelasDTO> pParcelas)
        {
            pPassivo.ValorOperacao = pParcelas.Sum(x => x.Valor_Parcela);

            return PassivoDAL.Inserir(pPassivo);
        }

        /// <summary>
        /// Método para Consultar um Passivo no Banco de Dados
        /// </summary>
        /// <param name="Id_Usuario"></param>
        /// <param name="Id_Divida"></param>
        /// <returns></returns>
        public DTO.PassivosDTO ConsultarDivida(int pId_Usuario, int pId_Passivo)
        {          
          return PassivoDAL.ConsultarPassivo( pId_Usuario,pId_Passivo);        
        }

        /// <summary>
        /// Método para Listar todos os Passivos do cliente
        /// </summary>
        /// <param name="pId_Usuario"></param>
        /// <returns></returns>
        public List<DTO.PassivosDTO> ListarPassivo(int pId_Usuario)
        {
            return PassivoDAL.ListarPassivo(pId_Usuario);              
        }

        /// <summary>
        /// Método para deletar Passivo e suas parcelas"Não Implemetando"
        /// </summary>
        /// <param name="pId_Usuario"></param>
        /// <param name="pId_Passivo"></param>
        public void DeletarPassivo(int pId_Passivo)
        {            
            PassivoDAL.DeletarPassivo(pId_Passivo);
        }

        /// <summary>
        /// Método para Alterar um Passivo
        /// </summary>
        /// <param name="pPassivo"></param>
        public void AlterarPassivo(PassivosDTO pPassivo) 
        {
            PassivoDAL.AlterarPassivo(pPassivo);
        }

        /// <summary>
        /// Converte Viewmodel em Objeto
        /// </summary>
        /// <param name="pPassivo"></param>
        /// <returns></returns>
        public PassivosDTO ConvertModeltoObj(Models.OperacaoModel pPassivo) 
        {
            PassivosDTO objPassivo = new PassivosDTO();
            UsuarioBLL bllUsuario = new UsuarioBLL();

            objPassivo.Id_Usuario = bllUsuario.ConsultarUsuarioSessao();
            objPassivo.Id_Operacao = pPassivo.IdOperacao;
            objPassivo.NomeOperacao = pPassivo.NomeOperacao;
            objPassivo.QtdParcela = pPassivo.QtdParcela;
            objPassivo.ValorParcela = pPassivo.ValorParcela;
            objPassivo.Descriacao = pPassivo.Descricao;
            objPassivo.DataVencimento = pPassivo.DataOperacao;
            objPassivo.DataAltercao = DateTime.Now;
            objPassivo.TipoCalculo = pPassivo.TipodeCalculo;
            
            return objPassivo;
        }

        #region Parcelas

        /// <summary>
        /// Método para Inserir um Passivo no Banco de Dados
        /// </summary>
        /// <param name="DTO"></param>        
        public void InserirParcela(List<OperacaoParcelasDTO> pListaParcelas,int pIdDivida)
        {
            PassivoDAL.InserirParcela(pListaParcelas, pIdDivida);
        }

        /// <summary>
        /// Método para consultar todas as parcelas de um passivo
        /// </summary>
        /// <param name="pIdDivida"></param>
        /// <returns></returns>
        public List<OperacaoParcelasDTO> ConsultarParcelas(int pIdDivida)
        {
            return PassivoDAL.ConsultarParcelas(pIdDivida);
        }
        
        #endregion

        public List<OperacaoParcelasDTO> GerarParcelas(PassivosDTO pPassivo)
        {
            List<OperacaoParcelasDTO> lista = new List<OperacaoParcelasDTO>();
                       
            for (int x = 1; x <= pPassivo.QtdParcela; x++)
            {
                OperacaoParcelasDTO objParcela = new OperacaoParcelasDTO();

                objParcela.Numero_Parcela = x;
                objParcela.Valor_Parcela = pPassivo.TipoCalculo == TipodeCalculo.parcela? pPassivo.ValorParcela : pPassivo.ValorParcela/pPassivo.QtdParcela ;
                objParcela.DataVencimentoParcela = x == 1 ? pPassivo.DataVencimento : pPassivo.DataVencimento.AddMonths(x-1);
                objParcela.Status = 1;
                
                lista.Add(objParcela);
            }        

            return lista;
        }

        #endregion
    }
}