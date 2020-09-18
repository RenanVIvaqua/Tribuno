using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Tribuno3.Camadas.DTO;
namespace Tribuno3.Models
{
    public class OperacaoModel
    {
        public int IdUsuario { get; set; }

        public int IdOperacao { get; set; }

        [Required][MaxLength(30)]
        public string NomeOperacao { get; set; }

        [Required]
        [Display(Name = "Valor da parcela")]
        public decimal ValorParcela { get; set; }
                
       // public TipodeCalculo TipodeCalculo { get; set; }
        
        [Required]
        [Display(Name = "Qtd. de Parcela")]
        [Range(minimum:0,maximum:60,ErrorMessage = "Valor permitido entre 0 e 60")]
        public int QtdParcela { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "1º Vencimento")]
        public DateTime DataOperacao { get; set; }

        [MaxLength(200)]
        public string Descricao { get; set; }  
        
        public TipoOperacao TipoOperacao { get; set; }

    }

    public class OperacaolModal
    {
        [Required]
        public int IdOperacao { get; set; }

        public bool Cadastro { get; set; }

        public TipoOperacao TipoOperacao { get; set; }
    }   
}