using System.ComponentModel.DataAnnotations;

namespace Forum1.Models.ContaViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "voce precisa ter o seu nome de usuario")]
        [Display(Name = "Nome de Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Voce precisa ter um email")]
        [EmailAddress(ErrorMessage = "Esse email não e valido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Voce precisa digitar a sua senha")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }
        [Display(Name = "Lembrar-me")]
        public bool Remenberme { get; set; }
    }
}