using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Tribuno3.Models
{
    public class OperacaoModel
    {
        public int IdUsuario { get; set; }

        public int IdOperacao { get; set; }

        [Required][MaxLength(30)]
        public string NomeOperacao { get; set; }

        [Required]
        public double ValorParcela { get; set; }
                
        public TipodeCalculo TipodeCalculo { get; set; }
        
        [Required]
        public int QtdParcela { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Data Operação")]
        public DateTime DataOperacao { get; set; }

        [MaxLength(200)]
        public string Descricao { get; set; }        

    }

    public class OperacaolModal
    {
        [Required]
        public int Id_Operacao { get; set; }

        public bool Cadastro { get; set; }

        public TipoOperacao TipoOperacao { get; set; }
    }    
    public enum TipoOperacao
    {
        [Description("Tipo de Operação Não Definido")]
        NaoDefinido,

        [Description("Tipo de Operação Ativo")]
        Rendimento, 

        [Description("Tipo de Operação Passivo")]
        Passivo        
      
    }

    public enum TipodeCalculo
    {    
        [Description("Calculado por Parcela")]
        parcela,

        [Description("Calculado por Valor Total da Operacao")]
        opercacao
    }

}