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
        [Required(ErrorMessage = "Informe o seu login.")]
        [MaxLength(20)]
        [Display(Name = "Login")]
        public string LoginUsuario { get; set; }

        [Required(ErrorMessage = "Informe a sua senha.")]
        [MaxLength(20)]
        [Display(Name = "Senha")]
        public string SenhaUsuario { get; set; }

    }

    public class UsuarioCadastro
    {
        [Required(ErrorMessage = "Informe o seu nome.")]
        [MaxLength(90, ErrorMessage = "O nome deve ter até {1} carateres.")]
        [Display(Name = "Nome")]
        public string NomeUsuario { get; set; }

        [Required(ErrorMessage = "Informe o seu Login.")]
        [MaxLength(20, ErrorMessage = "O login deve ter até {1} carateres.")]
        [Display(Name = "Login")]
        public string LoginUsuario { get; set; }

        [Required(ErrorMessage = "Informe a sua senha.")]
        [MaxLength(20, ErrorMessage = "A senha deve ter até {1} carateres.")]
        [Display(Name = "Senha")]
        public string SenhaUsuario { get; set; }
    
        [MaxLength(90, ErrorMessage = "O email deve ter até {1} carateres.")]
        public string Email { get; set; }
            
        public DateTime DataCadastro { get; set; }

    }
}