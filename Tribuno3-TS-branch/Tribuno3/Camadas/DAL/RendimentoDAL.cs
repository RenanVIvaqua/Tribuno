using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tribuno3.Camadas.DAL;
using Tribuno3.Camadas.DTO;
using Tribuno3.Camadas.BLL;

namespace Tribuno3.Camadas.DAL
{
    public class RendimentoDAL
    {
        #region Propriedades
        protected AcessoDados Acesso = new AcessoDados();  
        protected PrincipalBLL Generico = new PrincipalBLL();
        protected Dictionary<string, string> pParam = new Dictionary<string, string>();
        #endregion

        #region Métodos 
        
        /// <summary>
        /// Método Para Inserir Rendimento
        /// </summary>
        /// <param name="pRendimento"></param>
        public string Inserir(DTO.RendimentoDTO pRendimento)
        {
            pParam.Add("ID_USUARIO", pRendimento.Id_Usuario.ToString());
            pParam.Add("NM_RENDIMENTO", pRendimento.NomeOperacao.ToString());
            pParam.Add("VL_RENDIMENTO", pRendimento.ValorOperacao.ToString());
            pParam.Add("QTD_RENDIMENTO", pRendimento.QtdParcela.ToString());
            pParam.Add("REND_DT_CONTABIL", pRendimento.DataRecebimento.ToString("M/d/yyyy HH:mm:ss"));    
            if(!string.IsNullOrEmpty(pRendimento.Descriacao))
                pParam.Add("DESCRICAO", pRendimento.Descriacao);
            else
                pParam.Add("DESCRICAO", string.Empty);

            return Acesso.Executar(Processos.Executar.Inserir_Rendimento, Generico.ParametroSql(pParam));                     
        }

        /// <summary>
        /// Método para Consultar Rendimento
        /// </summary>
        /// <param name="pId_Usuario"></param>
        /// <param name="pId_Rendimento"></param>
        /// <returns></returns>
        public DTO.RendimentoDTO ConsultarRendimento(int pId_Usuario,int pId_Rendimento)
        {
            DTO.RendimentoDTO rendimento = new RendimentoDTO();

            pParam.Add("ID_USUARIO", pId_Usuario.ToString());
            pParam.Add("ID_RENDIMENTO", pId_Rendimento.ToString());           

            var ds = Acesso.Consultar(Tribuno3.Processos.Executar.Consultar_Rendimento, Generico.ParametroSql(pParam));

            if (ds.Rows[0].ItemArray[0] != DBNull.Value)
                rendimento.Id_Usuario = Convert.ToInt32(ds.Rows[0].ItemArray[0]);

            if (ds.Rows[0].ItemArray[1] != DBNull.Value)
                rendimento.Id_Operacao = Convert.ToInt32(ds.Rows[0].ItemArray[1]);

            if (ds.Rows[0].ItemArray[2] != DBNull.Value)
                rendimento.NomeOperacao = Convert.ToString(ds.Rows[0].ItemArray[2]);

            if (ds.Rows[0].ItemArray[3] != DBNull.Value)
                rendimento.ValorParcela = Convert.ToDouble(ds.Rows[0].ItemArray[3]);

            if (ds.Rows[0].ItemArray[4] != DBNull.Value)
                rendimento.QtdParcela = Convert.ToInt32(ds.Rows[0].ItemArray[4]);

            if (ds.Rows[0].ItemArray[5] != DBNull.Value)
                rendimento.DataRecebimento = Convert.ToDateTime(ds.Rows[0].ItemArray[5]);

            if (ds.Rows[0].ItemArray[6] != DBNull.Value)
                rendimento.Descriacao = Convert.ToString(ds.Rows[0].ItemArray[6]);

            if (ds.Rows[0].ItemArray[7] != DBNull.Value)
                rendimento.DataCriacaoOper = Convert.ToDateTime(ds.Rows[0].ItemArray[7]);

            if (ds.Rows[0].ItemArray[8] != DBNull.Value)
                rendimento.DataAltercao = Convert.ToDateTime(ds.Rows[0].ItemArray[8]);

            return rendimento;

        }

        /// <summary>
        /// Método para Listar todos os Rendimento do cliente
        /// </summary>
        /// <param name="pParam"></param>
        /// <returns></returns>
        public List<DTO.RendimentoDTO> ListarRendimento(int pId_Usuario)
        {
            List<DTO.RendimentoDTO> objLista = new List<DTO.RendimentoDTO>();
            pParam.Add("ID_USUARIO", pId_Usuario.ToString());

            var tabela = Acesso.Consultar(Processos.Executar.Consultar_Listar_Rendimento, Generico.ParametroSql(pParam));

            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                DTO.RendimentoDTO rendimento = new RendimentoDTO();

                if (tabela.Rows[i].ItemArray[0] != DBNull.Value)
                    rendimento.Id_Usuario = Convert.ToInt32(tabela.Rows[i].ItemArray[0]);

                if (tabela.Rows[i].ItemArray[1] != DBNull.Value)
                    rendimento.Id_Operacao = Convert.ToInt32(tabela.Rows[i].ItemArray[1]);

                if (tabela.Rows[i].ItemArray[2] != DBNull.Value)
                    rendimento.NomeOperacao = Convert.ToString(tabela.Rows[i].ItemArray[2]);

                if (tabela.Rows[i].ItemArray[3] != DBNull.Value)
                    rendimento.QtdParcela = Convert.ToInt32(tabela.Rows[i].ItemArray[3]);

                if (tabela.Rows[i].ItemArray[4] != DBNull.Value)
                    rendimento.DataRecebimento = Convert.ToDateTime(tabela.Rows[i].ItemArray[4]);

                if (tabela.Rows[i].ItemArray[5] != DBNull.Value)
                    rendimento.ValorOperacao = Convert.ToDouble(tabela.Rows[i].ItemArray[5]);

                if (tabela.Rows[i].ItemArray[6] != DBNull.Value)
                    rendimento.ValorParcela = Convert.ToDouble(tabela.Rows[i].ItemArray[6]);

                if (tabela.Rows[i].ItemArray[7] != DBNull.Value)
                    rendimento.Descriacao = Convert.ToString(tabela.Rows[i].ItemArray[7]);

                if (tabela.Rows[i].ItemArray[8] != DBNull.Value)
                    rendimento.DataCriacaoOper = Convert.ToDateTime(tabela.Rows[i].ItemArray[8]);

                if (tabela.Rows[i].ItemArray[9] != DBNull.Value)
                    rendimento.DataAltercao = Convert.ToDateTime(tabela.Rows[i].ItemArray[9]);

                objLista.Add(rendimento);
            }

            return objLista;
        }

        /// <summary>
        /// Método para desabilitar o Rendimento "Ainda não implementado"
        /// </summary>
        /// <param name="pParam"></param>
        /// <returns></returns>
        public void DeletarRendimento(int pId_Rendimento)
        {
            Dictionary<string, string> pParam = new Dictionary<string, string>();
            pParam.Add("ID_RENDIMENTO", pId_Rendimento.ToString());
            Acesso.Executar(Processos.Executar.Deletar_Rendimento, Generico.ParametroSql(pParam));            
        }

        /// <summary>
        /// Método para alterar os registro do Rendimento 
        /// </summary>
        /// <param name="pRendimento"></param>
        public void AlterarRendimento(DTO.RendimentoDTO pRendimento)
        {
            pParam.Add("IdUsuario", pRendimento.Id_Usuario.ToString());
            pParam.Add("IdRendimento", pRendimento.Id_Operacao.ToString());
            pParam.Add("NomeRendimento", pRendimento.NomeOperacao.ToString());
            pParam.Add("ValorRendimento", pRendimento.ValorParcela.ToString());
            pParam.Add("QtdParcelas", pRendimento.QtdParcela.ToString());
            pParam.Add("PassivoDataReceb", pRendimento.DataRecebimento.ToString("M/d/yyyy HH:mm:ss"));
            pParam.Add("Descricao", pRendimento.Descriacao.ToString());
            pParam.Add("DataAlteracao", pRendimento.DataAltercao.ToString("M/d/yyyy HH:mm:ss"));

            Acesso.Executar(Processos.Executar.Alterar_Rendimento, Generico.ParametroSql(pParam));

        }

        public List<OperacaoParcelasDTO> ConsultarParcelas(int pIdRendimento)
        {
            Dictionary<string, string> pParam = new Dictionary<string, string>();
            pParam.Add("ID_RENDIMENTO", pIdRendimento.ToString());

            var tabela = Acesso.Consultar(Processos.Executar.Consultar_Passivo_Parcela_Rendimento, Generico.ParametroSql(pParam));

            List<OperacaoParcelasDTO> listaParcelas = new List<OperacaoParcelasDTO>();

            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                OperacaoParcelasDTO objParcela = new OperacaoParcelasDTO();

                if (tabela.Rows[i].ItemArray[0] != DBNull.Value)
                    objParcela.Id_Operacao = Convert.ToInt32(tabela.Rows[i].ItemArray[0]);

                if (tabela.Rows[i].ItemArray[1] != DBNull.Value)
                    objParcela.Id_Parcela = Convert.ToInt32(tabela.Rows[i].ItemArray[1]);

                if (tabela.Rows[i].ItemArray[2] != DBNull.Value)
                    objParcela.Numero_Parcela = Convert.ToInt32(tabela.Rows[i].ItemArray[2]);

                if (tabela.Rows[i].ItemArray[3] != DBNull.Value)
                    objParcela.Valor_Parcela = Convert.ToInt32(tabela.Rows[i].ItemArray[3]);

                if (tabela.Rows[i].ItemArray[4] != DBNull.Value)
                    objParcela.DataVencimentoParcela = Convert.ToDateTime(tabela.Rows[0].ItemArray[4]);

                if (tabela.Rows[i].ItemArray[5] != DBNull.Value)
                    objParcela.Status = Convert.ToInt32(tabela.Rows[i].ItemArray[5]);

                listaParcelas.Add(objParcela);
            }

            return listaParcelas;
        }

        /// <summary>
        /// Método para inserir Parcelas 
        /// </summary>
        /// <param name="pListaParcela"></param>
        public void InserirParcela(List<OperacaoParcelasDTO> pListaParcela, int pIdRendimento)
        {
            foreach (var item in pListaParcela)
            {

                Dictionary<string, string> pParam = new Dictionary<string, string>();

                pParam.Add("ID_RENDIMENTO", pIdRendimento.ToString());
                pParam.Add("NUMERO_PARCELA", item.Numero_Parcela.ToString());
                pParam.Add("VL_PARCELA", item.Valor_Parcela.ToString());
                pParam.Add("DT_CONTABIL", item.DataVencimentoParcela.ToString("M/d/yyyy HH:mm:ss"));
                pParam.Add("TP_STATUS", item.Status.ToString());

                Acesso.Executar(Processos.Executar.Inserir_Parcela_Rendimento, Generico.ParametroSql(pParam));

            }
        }
        #endregion

    }
}