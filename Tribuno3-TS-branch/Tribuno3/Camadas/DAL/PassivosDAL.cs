using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tribuno3.Camadas.DAL;
using Tribuno3.Camadas.DTO;
using Tribuno3.Camadas.BLL;

namespace Tribuno3.Camadas.DAL
{
    public class PassivosDAL 
    {
        #region Atributos
        protected AcessoDados Acesso = new AcessoDados();
        protected DTO.PassivosDTO Passivos = new PassivosDTO();
        protected List<DTO.PassivosDTO> objLista = new List<DTO.PassivosDTO>();
        protected PrincipalBLL Generico = new PrincipalBLL();       
        #endregion

        /// <summary>
        /// Método para Listar todos os passivos do cliente
        /// </summary>
        /// <param name="pId_Usuario"></param>
        /// <returns></returns>
        public List<DTO.PassivosDTO> ListarPassivo(int pId_Usuario)
        {
            Dictionary<string, string> pParam = new Dictionary<string, string>();

            pParam.Add("ID_USUARIO", pId_Usuario.ToString());

            var tabela = Acesso.Consultar(Processos.Executar.Consultar_Listar_Passivo, Generico.ParametroSql(pParam));           

            for (int i = 0; i < tabela.Rows.Count; i++)
            {
                PassivosDTO Passivos = new PassivosDTO();

                var x = Convert.ToString(tabela.Rows[i].ItemArray[2]);

                if (tabela.Rows[i].ItemArray[0] != DBNull.Value)
                    Passivos.Id_Usuario = Convert.ToInt32(tabela.Rows[i].ItemArray[0]);

                if (tabela.Rows[i].ItemArray[1] != DBNull.Value)
                    Passivos.Id_Operacao = Convert.ToInt32(tabela.Rows[i].ItemArray[1]);

                if (tabela.Rows[i].ItemArray[2] != DBNull.Value)
                    Passivos.NomeOperacao = Convert.ToString(tabela.Rows[i].ItemArray[2]);

                if (tabela.Rows[i].ItemArray[3] != DBNull.Value)
                    Passivos.QtdParcela = Convert.ToInt32(tabela.Rows[i].ItemArray[3]);

                if (tabela.Rows[i].ItemArray[4] != DBNull.Value)
                    Passivos.DataVencimento = Convert.ToDateTime(tabela.Rows[i].ItemArray[4]);

                if (tabela.Rows[i].ItemArray[5] != DBNull.Value)
                    Passivos.ValorOperacao = Convert.ToDouble(tabela.Rows[i].ItemArray[5]);

                if (tabela.Rows[i].ItemArray[6] != DBNull.Value)
                    Passivos.ValorParcela = Convert.ToDouble(tabela.Rows[i].ItemArray[6]);                               

                if (tabela.Rows[i].ItemArray[7] != DBNull.Value)
                    Passivos.Descriacao = Convert.ToString(tabela.Rows[i].ItemArray[7]);

                if (tabela.Rows[i].ItemArray[8] != DBNull.Value)
                    Passivos.DataCriacaoOper = Convert.ToDateTime(tabela.Rows[i].ItemArray[8]);

                if (tabela.Rows[i].ItemArray[9] != DBNull.Value)
                    Passivos.DataAltercao = Convert.ToDateTime(tabela.Rows[i].ItemArray[9]);

                objLista.Add(Passivos);
                
            }

            return objLista;

        }

        /// <summary>
        /// Método para incluir Passivo
        /// </summary>
        /// <param name="pPassivo"></param>
        public string Inserir(DTO.PassivosDTO pPassivo)
        {
            Dictionary<string, string> pParam = new Dictionary<string, string>();

            pParam.Add("ID_USUARIO", pPassivo.Id_Usuario.ToString());
            pParam.Add("NM_DIVIDA", pPassivo.NomeOperacao.ToString());
            pParam.Add("VL_DIVIDA", pPassivo.ValorOperacao.ToString());
            pParam.Add("QTD_PARCELA", pPassivo.QtdParcela.ToString());
            pParam.Add("DIVIDA_DT_VENCIMENTO", pPassivo.DataVencimento.ToString("yyyy/MM/d"));
            if (!string.IsNullOrEmpty(pPassivo.Descriacao))
                pParam.Add("DESCRICAO", pPassivo.Descriacao.ToString()); 
            else
                pParam.Add("DESCRICAO", string.Empty);

            return Acesso.Executar(Processos.Executar.Inserir_Passivo, Generico.ParametroSql(pParam));

        }

        /// <summary>
        /// Método para retornar objeto Passivo 
        /// </summary>
        /// <param name="pParam"></param>
        /// <returns></returns>
        public DTO.PassivosDTO ConsultarPassivo(int pId_Usuario,int pId_Passivo)
        {
            Dictionary<string, string> pParam = new Dictionary<string, string>();

            pParam.Add("ID_USUARIO", pId_Usuario.ToString());
            pParam.Add("ID_DIVIDA", pId_Passivo.ToString());

            var ds = Acesso.Consultar(Tribuno3.Processos.Executar.Consultar_Divida, Generico.ParametroSql(pParam));

            if (ds.Rows[0].ItemArray[0] != DBNull.Value)
                Passivos.Id_Usuario = Convert.ToInt32(ds.Rows[0].ItemArray[0]);

            if (ds.Rows[0].ItemArray[1] != DBNull.Value)
                Passivos.Id_Operacao = Convert.ToInt32(ds.Rows[0].ItemArray[1]);

            if (ds.Rows[0].ItemArray[2] != DBNull.Value)
                Passivos.NomeOperacao = Convert.ToString(ds.Rows[0].ItemArray[2]);

            if (ds.Rows[0].ItemArray[3] != DBNull.Value)
                Passivos.ValorParcela = Convert.ToDouble(ds.Rows[0].ItemArray[3]);

            if (ds.Rows[0].ItemArray[4] != DBNull.Value)
                Passivos.QtdParcela = Convert.ToInt32(ds.Rows[0].ItemArray[4]);

            if (ds.Rows[0].ItemArray[5] != DBNull.Value)
                Passivos.DataVencimento = Convert.ToDateTime(ds.Rows[0].ItemArray[5]);

            if (ds.Rows[0].ItemArray[6] != DBNull.Value)
                Passivos.Descriacao = Convert.ToString(ds.Rows[0].ItemArray[6]);

            if (ds.Rows[0].ItemArray[7] != DBNull.Value)
                Passivos.DataCriacaoOper = Convert.ToDateTime(ds.Rows[0].ItemArray[7]);

            if (ds.Rows[0].ItemArray[8] != DBNull.Value)
                Passivos.DataAltercao = Convert.ToDateTime(ds.Rows[0].ItemArray[8]);

            return Passivos;
        }

        /// <summary>
        /// Método para desabilitar o passivo "Ainda não implementado"
        /// </summary>
        /// <param name="pParam"></param>
        /// <returns></returns>
        public void DeletarPassivo(int pId_Passivo)
        {           
            Dictionary<string, string> pParam = new Dictionary<string, string>();           
            pParam.Add("Id_Passivo", pId_Passivo.ToString());
            Acesso.Executar(Processos.Executar.Deletar_Passivo, Generico.ParametroSql(pParam));

        }
        public void AlterarPassivo(DTO.PassivosDTO pPassivo)
        {
            Dictionary<string, string> pParam = new Dictionary<string, string>();

            pParam.Add("IdUsuario", pPassivo.Id_Usuario.ToString());
            pParam.Add("IdPassivo", pPassivo.Id_Operacao.ToString());
            pParam.Add("NomePassivo", pPassivo.NomeOperacao.ToString());
            pParam.Add("ValorPassivo", pPassivo.ValorParcela.ToString());
            pParam.Add("QtdParcelas", pPassivo.QtdParcela.ToString());
            pParam.Add("PassivoDataVenc", pPassivo.DataVencimento.ToString("M/d/yyyy HH:mm:ss"));
            pParam.Add("Descricao", pPassivo.Descriacao.ToString());
            pParam.Add("DataAlteracao", pPassivo.DataAltercao.ToString("M/d/yyyy HH:mm:ss"));

            Acesso.Executar(Processos.Executar.Alterar_Passivo, Generico.ParametroSql(pParam));

        }

        #region Parcelas

        /// <summary>
        /// Método para inserir Parcelas 
        /// </summary>
        /// <param name="pListaParcela"></param>
        public void InserirParcela(List<OperacaoParcelasDTO> pListaParcela,int pIdDivida)
        {             
            foreach (var item in pListaParcela)
            {

                Dictionary<string, string> pParam = new Dictionary<string, string>();

                pParam.Add("ID_DIVIDA", pIdDivida.ToString());
                pParam.Add("NUMERO_PARCELA", item.Numero_Parcela.ToString());
                pParam.Add("VL_PARCELA", item.Valor_Parcela.ToString());
                pParam.Add("PCL_VENCIMENTO", item.DataVencimentoParcela.ToString("M/d/yyyy HH:mm:ss"));
                pParam.Add("TP_STATUS", item.Status.ToString());

                Acesso.Executar(Processos.Executar.Inserir_Passivo_Parcela, Generico.ParametroSql(pParam));

            }       
        }

        public List<OperacaoParcelasDTO> ConsultarParcelas(int pIdDivida)
        {
            Dictionary<string, string> pParam = new Dictionary<string, string>();
            pParam.Add("ID_DIVIDA", pIdDivida.ToString());

            var tabela = Acesso.Consultar(Processos.Executar.Consultar_Passivo_Parcela, Generico.ParametroSql(pParam));

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



        #endregion

    }
}