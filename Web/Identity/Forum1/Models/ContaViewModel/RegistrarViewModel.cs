using System.ComponentModel.DataAnnotations;

namespace Forum1.Models.ContaViewModel
{
    public class RegistrarViewModel
    {
        [Required]
        [StringLength( maximumLength:30, MinimumLength=4,ErrorMessage = "Seu nome dever estar entre 4 e 30 caracteres!")]
        [Display(Name = "Nome de Usuario")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Seu email não e valido!")]
        
        public string Email { get; set; }

        [Required(ErrorMessage = "voce tem que ter uma senha!")]
        [DataType(DataType.Password)]
        [StringLength (80,ErrorMessage = "Sua senha deve ter entre 6 e 80 caracteres!")]
        [Display(Name = "Senha")]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "As senha não estão iguais!")]
        [Display(Name = "Comfirme a sua Senha")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Lembrar-me")]
        public bool Remenberme { get; set; }
    }
}