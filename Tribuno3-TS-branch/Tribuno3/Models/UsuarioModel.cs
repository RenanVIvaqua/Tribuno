using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace Tribuno3.Models
{
    /// <summary>
    /// Classe de Modelo de Usuário
    /// </summary>
    public class UsuarioModel
    {             
        [Required]
        [MaxLength(20)]
        public string LoginUsuario { get; set; }

        [Required]
        [MaxLength(20)]
        public string SenhaUsuario { get; set; }

    }

    public class UsuarioCadastro
    {
        [Required]
        [MaxLength(90)]
        public string NomeUsuario { get; set; }

        [Required]
        [MaxLength(20)]
        public string LoginUsuario { get; set; }

        [Required]
        [MaxLength(20)]
        public string SenhaUsuario { get; set; }
    
        [MaxLength(90)]
        public string Email { get; set; }
            
        public DateTime DataCadastro { get; set; }

    }
}