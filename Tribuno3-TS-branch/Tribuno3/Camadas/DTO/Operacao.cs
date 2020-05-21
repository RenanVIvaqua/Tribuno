using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Tribuno3.Camadas.DTO
{
    public class Operacao
    {
        protected int id_Usuario { get; set; }
        protected string nomeOperacao { get; set; }
        protected double valorOperacao { get; set; }
        protected double valorParcela { get; set; }
        protected int qtdParcela { get; set; }
        protected string descriacao { get; set; }
        protected DateTime dataCriacaoOper { get; set; }
        protected DateTime dataAltercao { get; set; }

    }    
    
    
}