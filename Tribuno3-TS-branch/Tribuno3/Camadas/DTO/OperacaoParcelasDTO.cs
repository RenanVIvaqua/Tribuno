using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Tribuno3.Camadas.DTO
{
    public class OperacaoParcelasDTO 
    {
        public int Id_Operacao { get; set; }
        public int Id_Parcela { get; set; }
        public int Numero_Parcela { get; set; }
        public double Valor_Parcela { get; set; }
        public DateTime DataVencimentoParcela { get; set; }
        public StatusParcela Status { get; set; }
    }

    public enum StatusParcela
    {
        [Description("Tipo de Parcela Não definido")]
        NaoDefinido = 1,

        [Description("Parcela Em Aberto")]
        EmAberto = 2,

        [Description("Parcela em atraso")]
        Vencido = 3,

        [Description("Parcela quitada")]
        Pago = 4

    }
}