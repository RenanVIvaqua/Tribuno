using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tribuno3.Camadas.DTO
{
    public class OperacaoParcelasDTO 
    {
        public int Id_Operacao;
        public int Id_Parcela;
        public int Numero_Parcela;
        public double Valor_Parcela;
        public DateTime DataVencimentoParcela;
        public int Status;
    }
}