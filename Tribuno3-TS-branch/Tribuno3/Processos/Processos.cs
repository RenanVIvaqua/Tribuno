using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tribuno3.Processos
{
    public class Executar
    {
        #region Procedures
        public static string  Consultar_Usuario = "PRC_Logar_Usuario";
        public static string  Consultar_Usuario_Id = "PRC_Consultar_Usuario";
        public static string  Consultar_NomeLogin  = "Prc_Consultar_NomeLogin";
        public static string  Consultar_Resumo_Financeiro_Mes = "PRC_RESUMO_SITUACAO_MES_DETALHE";
        public static string  Consultar_Divida = "PRC_Consultar_Divida";
        public static string  Consultar_Rendimento = "PRC_Consultar_Rendimento"; 
        public static string  Consultar_Listar_Passivo = "PRC_Listar_Divida";
        public static string  Consultar_Listar_Rendimento = "PRC_Listar_Rendimento";
        public static string  Consultar_Passivo_Parcela = "Prc_Consultar_Parcelas";
        public static string  Consultar_Passivo_Parcela_Rendimento = "Prc_Consultar_Parcelas_Rendimento";
                            
        public static string  Inserir_Passivo_Parcela = "Prc_Inserir_Parcelas";
        public static string  Inserir_Usuario = "PRC_Cadastrar_Usuario";
        public static string  Inserir_Passivo = "PRC_Inserir_Divida";
        public static string  Inserir_Parcela_Rendimento = "Prc_Inserir_Parcelas_Rendimento";
        public static string  Inserir_Rendimento  = "PRC_Inserir_Rendimento";

        public static string  Alterar_Passivo = "Prc_Alterar_Divida";
        public static string  Alterar_Rendimento = "Prc_Alterar_Rendimento";

        public static string  Deletar_Passivo = "PRC_Deletar_Passivo";
        public static string  Deletar_Rendimento = "Prc_Deletar_Rendimento";

        #endregion

        #region Select


        #endregion
    }
}