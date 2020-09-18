using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tribuno3.Camadas.DTO;
using Tribuno3.Camadas.BLL;

namespace Tribuno3.Camadas.DAL
{
    public class OperacaoDAL
    {
        #region Atributos
        private AcessoDados Acesso = new AcessoDados();
        private OperacaoDTO Operacao = new OperacaoDTO();
        private List<OperacaoDTO> listaOperacao = new List<OperacaoDTO>();
        private BLL.Util Util = new BLL.Util();
        #endregion

        /// <summary>
        /// Método para Listar todos os passivos do cliente
        /// </summary>
        /// <param name="pId_Usuario"></param>
        /// <returns></returns>
        public List<OperacaoDTO> ListarOperacao(int pIdUsuario,TipoOperacao pTipoOperacao)
        {
            Dictionary<string, string> pParam = new Dictionary<string, string>();

            pParam.Add("IdUsuario", pIdUsuario.ToString());
            pParam.Add("TipoOperacao", Convert.ToString((int)pTipoOperacao));

            var tabela = Acesso.Consultar(Processos.Executar.Consultar_Listar_Operacao, Util.ParametroSql(pParam));

            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                OperacaoDTO operacao = new OperacaoDTO();
                               
                if (tabela.Rows[i]["IdUsuario"] != DBNull.Value)
                    operacao.IdUsuario = Convert.ToInt32(tabela.Rows[i]["IdUsuario"]);

                if (tabela.Rows[i]["IdOperacao"] != DBNull.Value)
                    operacao.IdOperacao = Convert.ToInt32(tabela.Rows[i]["IdOperacao"]);

                if (tabela.Rows[i]["NomeOperacao"] != DBNull.Value)
                    operacao.NomeOperacao = Convert.ToString(tabela.Rows[i]["NomeOperacao"]);

                if (tabela.Rows[i]["QtdParcela"] != DBNull.Value)
                    operacao.QtdParcela = Convert.ToInt32(tabela.Rows[i]["QtdParcela"]);              

                if (tabela.Rows[i]["ValorOperacao"] != DBNull.Value)
                    operacao.ValorOperacao = Convert.ToDouble(tabela.Rows[i]["ValorOperacao"]);

                if (tabela.Rows[i]["ValorParcela"] != DBNull.Value)
                    operacao.ValorParcela = Convert.ToDouble(tabela.Rows[i]["ValorParcela"]);

                if (tabela.Rows[i]["Descricao"] != DBNull.Value)
                    operacao.Descricao = Convert.ToString(tabela.Rows[i]["Descricao"]);

                if (tabela.Rows[i]["DataCadastro"] != DBNull.Value)
                    operacao.DataCadastro = Convert.ToDateTime(tabela.Rows[i]["DataCadastro"]);

                if (tabela.Rows[i]["DataAlteracao"] != DBNull.Value)
                    operacao.DataAlteracao = Convert.ToDateTime(tabela.Rows[i]["DataAlteracao"]);           

                if (tabela.Rows[i]["TipoOperacao"] != DBNull.Value)
                    operacao.TipoOperacao = (TipoOperacao)Convert.ToInt32(tabela.Rows[i]["TipoOperacao"]);

                if (tabela.Rows[i]["TipoCalculo"] != DBNull.Value)
                    operacao.TipoCalculo = (TipodeCalculo)Convert.ToInt32(tabela.Rows[i]["TipoCalculo"]);

                if (operacao.IdOperacao > 0)
                    operacao.Parcelas = new OperacaoBLL().ConsultarParcelas(operacao.IdOperacao);
                
                listaOperacao.Add(operacao);
            }
            return listaOperacao;
        }

        /// <summary>
        /// Método para incluir Passivo
        /// </summary>
        /// <param name="pPassivo"></param>
        public string InserirOperacao(OperacaoDTO pOperacao)
        {
            Dictionary<string, string> pParam = new Dictionary<string, string>();           
            pParam.Add("IdUsuario", pOperacao.IdUsuario.ToString());
            pParam.Add("NomeOperacao", pOperacao.NomeOperacao.ToString());
            pParam.Add("ValorOperacao", pOperacao.ValorOperacao.ToString());
            pParam.Add("ValorParcela", pOperacao.ValorParcela.ToString());
            pParam.Add("QtdParcela", pOperacao.QtdParcela.ToString());
            pParam.Add("Descricao", pOperacao.Descricao = pOperacao.Descricao!= null ? pOperacao.Descricao.ToString() : string.Empty);
            pParam.Add("DataCadastro", DateTime.Now.ToString("yyyy/M/d HH:mm:ss"));
            pParam.Add("TipoOperacao", Convert.ToString((int)pOperacao.TipoOperacao));
            pParam.Add("TipoCalculo", "2");

            return Acesso.Executar(Processos.Executar.Inserir_Operacao, Util.ParametroSql(pParam));
        }

        /// <summary>
        /// Método para retornar objeto Passivo 
        /// </summary>
        /// <param name="pParam"></param>
        /// <returns></returns>
        public OperacaoDTO ConsultarOperacao(int pIdUsuario, int pIdOperacao)
        {//criar parametro com o tipo de operação
            Dictionary<string, string> pParam = new Dictionary<string, string>();

            pParam.Add("IdUsuario", pIdUsuario.ToString());
            pParam.Add("IdOperacao", pIdOperacao.ToString());

            var ds = Acesso.Consultar(Tribuno3.Processos.Executar.Consultar_Operacao, Util.ParametroSql(pParam));

            if (ds.Rows[0]["IdUsuario"] != DBNull.Value)
                Operacao.IdUsuario = Convert.ToInt32(ds.Rows[0]["IdUsuario"]);

            if (ds.Rows[0]["IdOperacao"] != DBNull.Value)
                Operacao.IdOperacao = Convert.ToInt32(ds.Rows[0]["IdOperacao"]);

            if (ds.Rows[0]["NomeOperacao"] != DBNull.Value)
                Operacao.NomeOperacao = Convert.ToString(ds.Rows[0]["NomeOperacao"]);

            if (ds.Rows[0]["ValorParcela"] != DBNull.Value)
                Operacao.ValorParcela = Convert.ToDouble(ds.Rows[0]["ValorParcela"]);

            if (ds.Rows[0]["ValorOperacao"] != DBNull.Value)
                Operacao.ValorOperacao = Convert.ToDouble(ds.Rows[0]["ValorOperacao"]);

            if (ds.Rows[0]["QtdParcela"] != DBNull.Value)
                Operacao.QtdParcela = Convert.ToInt32(ds.Rows[0]["QtdParcela"]);
                      
            if (ds.Rows[0]["Descricao"] != DBNull.Value)
                Operacao.Descricao = Convert.ToString(ds.Rows[0]["Descricao"]);

            if (ds.Rows[0]["DataCadastro"] != DBNull.Value)
                Operacao.DataCadastro = Convert.ToDateTime(ds.Rows[0]["DataCadastro"]);

            if (ds.Rows[0]["DataAlteracao"] != DBNull.Value)
                Operacao.DataAlteracao = Convert.ToDateTime(ds.Rows[0]["DataAlteracao"]);

            if (ds.Rows[0]["TipoOperacao"] != DBNull.Value)
                Operacao.TipoOperacao = (TipoOperacao)Convert.ToInt32(ds.Rows[0]["TipoOperacao"]);

            if (ds.Rows[0]["TipoCalculo"] != DBNull.Value)
                Operacao.TipoCalculo = (TipodeCalculo)Convert.ToInt32(ds.Rows[0]["TipoCalculo"]);

            return Operacao;
        }

        /// <summary>
        /// Método para desabilitar o passivo "Ainda não implementado"
        /// </summary>
        /// <param name="pParam"></param>
        /// <returns></returns>
        public void DeletarOperacao(int pIdOperacao)
        {
            Dictionary<string, string> pParam = new Dictionary<string, string>();
            pParam.Add("IdOperacao", pIdOperacao.ToString());
            Acesso.Executar(Processos.Executar.Deletar_Operacao, Util.ParametroSql(pParam));
        }
        public void AlterarOperacao(OperacaoDTO pOperacao)
        {
            Dictionary<string, string> pParam = new Dictionary<string, string>();        

            pParam.Add("IdUsuario", pOperacao.IdUsuario.ToString());
            pParam.Add("NomeOperacao", pOperacao.NomeOperacao.ToString());
            pParam.Add("ValorOperacao", pOperacao.ValorOperacao.ToString());
            pParam.Add("ValorParcela", pOperacao.ValorParcela.ToString());
            pParam.Add("QtdParcela", pOperacao.QtdParcela.ToString());           
            pParam.Add("Descricao", pOperacao.Descricao.ToString());
            pParam.Add("DataAlteracao", pOperacao.DataAlteracao.ToString("yyyy/M/d HH:mm:ss"));
            pParam.Add("TipoOperacao", pOperacao.TipoOperacao.ToString());
            pParam.Add("TipoCalculo", pOperacao.TipoCalculo.ToString());

            Acesso.Executar(Processos.Executar.Alterar_Operacao, Util.ParametroSql(pParam));
        }

        #region Parcelas

        /// <summary>
        /// Método para inserir Parcelas 
        /// </summary>
        /// <param name="pListaParcela"></param>
        public void InserirParcela(List<OperacaoParcelasDTO> pListaParcela, int pIdDivida)
        {
            foreach (var item in pListaParcela)
            {
                Dictionary<string, string> pParam = new Dictionary<string, string>();

                pParam.Add("IdOperacao", pIdDivida.ToString());
                pParam.Add("NumeroParcela", item.Numero_Parcela.ToString());
                pParam.Add("Valor_Parcela", item.Valor_Parcela.ToString());
                pParam.Add("DataVencimento", item.DataVencimentoParcela.ToString("yyyy/M/d HH:mm:ss"));
                pParam.Add("StatusParcela", Convert.ToString((int)item.Status));

                Acesso.Executar(Processos.Executar.Inserir_Operacao_Parcela, Util.ParametroSql(pParam));
            }
        }

        public List<OperacaoParcelasDTO> ConsultarParcelas(int pIdOperacao)
        {
            Dictionary<string, string> pParam = new Dictionary<string, string>();
            pParam.Add("IdOperacao", pIdOperacao.ToString());

            var tabela = Acesso.Consultar(Processos.Executar.Consultar_Operacao_Parcela, Util.ParametroSql(pParam));

            List<OperacaoParcelasDTO> listaParcelas = new List<OperacaoParcelasDTO>();

            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                OperacaoParcelasDTO objParcela = new OperacaoParcelasDTO();

                if (tabela.Rows[i]["IdOperacao"] != DBNull.Value)
                    objParcela.Id_Operacao = Convert.ToInt32(tabela.Rows[i]["IdOperacao"]);

                if (tabela.Rows[i]["IdParcela"] != DBNull.Value)
                    objParcela.Id_Parcela = Convert.ToInt32(tabela.Rows[i]["IdParcela"]);

                if (tabela.Rows[i]["NumeroParcela"] != DBNull.Value)
                    objParcela.Numero_Parcela = Convert.ToInt32(tabela.Rows[i]["NumeroParcela"]);

                if (tabela.Rows[i]["Valor_Parcela"] != DBNull.Value)
                    objParcela.Valor_Parcela = Convert.ToInt32(tabela.Rows[i]["Valor_Parcela"]);

                if (tabela.Rows[i]["DataVencimento"] != DBNull.Value)
                    objParcela.DataVencimentoParcela = Convert.ToDateTime(tabela.Rows[i]["DataVencimento"]);

                if (tabela.Rows[i]["StatusParcela"] != DBNull.Value)
                    objParcela.Status = (StatusParcela)Convert.ToInt32(tabela.Rows[i]["StatusParcela"]);

                listaParcelas.Add(objParcela);
            }

            return listaParcelas;
        }



        #endregion

    }
}
