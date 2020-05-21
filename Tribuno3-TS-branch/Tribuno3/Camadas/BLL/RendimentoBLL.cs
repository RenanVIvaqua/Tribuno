using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tribuno3.Camadas.DAL;
using Tribuno3.Camadas.DTO;

namespace Tribuno3.Camadas.BLL
{
    public class RendimentoBLL
    {
        #region Propriedades
        protected AcessoDados Acesso = new AcessoDados();     
        protected PrincipalBLL Generico = new PrincipalBLL();
        protected RendimentoDAL RendimentoDAL = new RendimentoDAL();
        #endregion

        #region Métodos
        /// <summary>
        /// Método para Inserir Rendimento no Banco de Dados
        /// </summary>
        /// <param name="pRendimento"></param>
        public string InserirRendimento(RendimentoDTO pRendimento, List<OperacaoParcelasDTO> pParcelas)
         {
            pRendimento.ValorOperacao = pParcelas.Sum(x => x.Valor_Parcela);
            return RendimentoDAL.Inserir(pRendimento);
         }

        /// <summary>
         /// Método para Consultar Rendimento no Banco de Dados
         /// </summary>
         /// <param name="pId_Usuario"></param>
         /// <param name="pId_Rendimento"></param>
         /// <returns></returns>
        public DTO.RendimentoDTO ConsultarRendimento(int pId_Usuario, int pId_Rendimento)
        {          
            return RendimentoDAL.ConsultarRendimento(pId_Usuario, pId_Rendimento);
        }

        /// <summary>
        /// Método para Listar todos os Rendimentos do cliente
        /// </summary>
        /// <param name="pId_Usuario"></param>
        /// <returns></returns>
        public List<DTO.RendimentoDTO> ListarRendimento(int pId_Usuario)
        {
            return RendimentoDAL.ListarRendimento(pId_Usuario);
        }

        /// <summary>
        /// Método para desabilitar o Rendimento "Ainda não implementado"
        /// </summary>
        /// <param name="pParam"></param>
        /// <returns></returns>
        public void DeletarRendimento(int pId_Rendimento)
        {
            RendimentoDAL.DeletarRendimento(pId_Rendimento);
        }

        /// <summary>
        /// Método para Alterar um Rendimento
        /// </summary>
        /// <param name="pRendimento"></param>
        public void AlterarRendimento(RendimentoDTO pRendimento)
        {
            RendimentoDAL.AlterarRendimento(pRendimento);
        }

        /// <summary>
        /// Converte Viewmodel em Objeto
        /// </summary>
        /// <param name="pRendimento"></param>
        /// <returns></returns>
        public RendimentoDTO ConvertModeltoObj(Models.OperacaoModel pRendimento)
        {
            RendimentoDTO objRendimento = new RendimentoDTO();
            UsuarioBLL bllUsuario = new UsuarioBLL();

            objRendimento.Id_Usuario = bllUsuario.ConsultarUsuarioSessao();
            objRendimento.Id_Operacao = pRendimento.IdOperacao;
            objRendimento.NomeOperacao = pRendimento.NomeOperacao;
            objRendimento.QtdParcela = pRendimento.QtdParcela;
            objRendimento.ValorParcela = pRendimento.ValorParcela;
            objRendimento.Descriacao = pRendimento.Descricao;
            objRendimento.DataRecebimento = pRendimento.DataOperacao;
            objRendimento.DataAltercao = DateTime.Now;
            objRendimento.TipoCalculo = pRendimento.TipodeCalculo;

            return objRendimento;
        }
                 
        #endregion

        #region Parcelas

        /// <summary>
        /// Método para Inserir um Passivo no Banco de Dados
        /// </summary>
        /// <param name="DTO"></param>        
        public void InserirParcela(List<OperacaoParcelasDTO> pListaParcelas, int pIdDivida)
        {
            RendimentoDAL.InserirParcela(pListaParcelas, pIdDivida);
        }

        /// <summary>
        /// Método para consultar todas as parcelas de um passivo
        /// </summary>
        /// <param name="pIdDivida"></param>
        /// <returns></returns>
        public List<OperacaoParcelasDTO> ConsultarParcelas(int pIdDivida)
        {
            return RendimentoDAL.ConsultarParcelas(pIdDivida);
        }

        #endregion
    }
}