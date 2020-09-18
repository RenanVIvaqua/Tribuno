using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tribuno3.Camadas.DAL;
using Tribuno3.Camadas.DTO;
using Tribuno3.Models;

namespace Tribuno3.Camadas.BLL
{
    public class OperacaoBLL
    {
        #region Propriedades     
        private OperacaoDAL OperacaoDAL = new OperacaoDAL();        
        #endregion               

        #region Métodos

        /// <summary>
        /// Método para Inserir um Passivo no Banco de Dados
        /// </summary>
        /// <param name="DTO"></param>        
        public string InserirOperacao(OperacaoDTO pOperacao)
        {     
            return OperacaoDAL.InserirOperacao(pOperacao);
        }

        /// <summary>
        /// Método para Consultar um Passivo no Banco de Dados
        /// </summary>
        /// <param name="Id_Usuario"></param>
        /// <param name="Id_Divida"></param>
        /// <returns></returns>
        public OperacaoDTO ConsultarOperacao(int pIdUsuario, int pIdOperacao)
        {
             var operacao = OperacaoDAL.ConsultarOperacao(pIdUsuario, pIdOperacao);
             operacao.Parcelas = ConsultarParcelas(pIdOperacao);
             return operacao;
        }

        /// <summary>
        /// Método para Listar todos os Passivos do cliente
        /// </summary>
        /// <param name="pId_Usuario"></param>
        /// <returns></returns>
        public List<OperacaoDTO> ListarOperacao(int pIdUsuario, TipoOperacao pTipoOperacao)
        {
            return OperacaoDAL.ListarOperacao(pIdUsuario, pTipoOperacao);
        }

        /// <summary>
        /// Método para deletar Passivo e suas parcelas"Não Implemetando"
        /// </summary>
        /// <param name="pId_Usuario"></param>
        /// <param name="pId_Passivo"></param>
        public void DeletarOperacao(int pId_Passivo)
        {
            OperacaoDAL.DeletarOperacao(pId_Passivo);
        }

        /// <summary>
        /// Método para Alterar um Passivo
        /// </summary>
        /// <param name="pPassivo"></param>
        public void AlterarPassivo(OperacaoDTO pOperacao)
        {
            OperacaoDAL.AlterarOperacao(pOperacao);
        }

        /// <summary>
        /// Converte Viewmodel em Objeto
        /// </summary>
        /// <param name="pPassivo"></param>
        /// <returns></returns>
        public OperacaoDTO ConvertModeltoObj(Models.OperacaoModel pModelOperacao)
        {
            OperacaoDTO operacaoDTO = new OperacaoDTO();
            UsuarioBLL usuarioBLL = new UsuarioBLL();

            operacaoDTO.IdUsuario = usuarioBLL.ConsultarUsuarioSessao();
            operacaoDTO.IdOperacao = pModelOperacao.IdOperacao;
            operacaoDTO.NomeOperacao = pModelOperacao.NomeOperacao;
            operacaoDTO.QtdParcela = pModelOperacao.QtdParcela;
            operacaoDTO.ValorParcela = (double)pModelOperacao.ValorParcela;
            operacaoDTO.Descricao = pModelOperacao.Descricao;            
            operacaoDTO.DataAlteracao = DateTime.Now;           
            operacaoDTO.TipoOperacao = pModelOperacao.TipoOperacao;

            //operacaoDTO.TipoCalculo = pModelOperacao.TipodeCalculo;
            operacaoDTO.TipoCalculo = TipodeCalculo.Parcela;

            return operacaoDTO;
        }

        #region Parcelas

        /// <summary>
        /// Método para Inserir um Passivo no Banco de Dados
        /// </summary>
        /// <param name="DTO"></param>        
        public void InserirParcela(List<OperacaoParcelasDTO> pListaParcelas, int pIdDivida)
        {
            OperacaoDAL.InserirParcela(pListaParcelas, pIdDivida);
        }

        /// <summary>
        /// Método para consultar todas as parcelas de um passivo
        /// </summary>
        /// <param name="pIdDivida"></param>
        /// <returns></returns>
        public List<OperacaoParcelasDTO> ConsultarParcelas(int pIdOperacao)
        {
            return OperacaoDAL.ConsultarParcelas(pIdOperacao);
        }

        #endregion

        public List<OperacaoParcelasDTO> GerarParcelas(OperacaoModel pOperacao)
        {
            List<OperacaoParcelasDTO> lista = new List<OperacaoParcelasDTO>();

            for (int x = 1; x <= pOperacao.QtdParcela; x++)
            {
                OperacaoParcelasDTO objParcela = new OperacaoParcelasDTO();

                objParcela.Numero_Parcela = x;
                // Sera ajustado no Futuro ! 
                // decimal valorParcela = pOperacao.TipodeCalculo == TipodeCalculo.Parcela ? (decimal)pOperacao.ValorParcela : (decimal)pOperacao.ValorParcela / pOperacao.QtdParcela;
                decimal valorParcela = (decimal)pOperacao.ValorParcela;

                objParcela.Valor_Parcela = (double)valorParcela;
                objParcela.DataVencimentoParcela = x == 1 ? pOperacao.DataOperacao : pOperacao.DataOperacao.AddMonths(x - 1);
                objParcela.Status = StatusParcela.EmAberto;

                lista.Add(objParcela);
            }

            return lista;
        }

        #endregion
    }
}