
using System.ComponentModel.DataAnnotations;

namespace TestAuth.Models.AccountViewModel{
    public class RegisterViewModel{

        [Required(ErrorMessage = "vc precisa ter um email para se registrar")]
        [EmailAddress(ErrorMessage = "esse email não e valido!")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "vc precisa ter uma senha!")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        [StringLength(80,  ErrorMessage = "voce passou do limite de senha(80),por favor peço que diminual a sua senha")]
        public string Password { get; set; }

        [Required(ErrorMessage = "vc precisa colocar uma senha de confirmação!")]
        [DataType(DataType.Password)]
        [Display(Name = "Cofirmar senha")]
        [Compare("Password", ErrorMessage="as senhas não estão iguais!")]
        public string ConfirmPassword { get; set; }
    }
}