using System.ComponentModel.DataAnnotations;

namespace TestAuth.Models.ManagerViewModel
{
    public class ChangePassWordViewModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Senha Atual")]
        public string OldPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Nova senha")]
        [StringLength (100, ErrorMessage = "A sua nova senha so pode ir de 6 ate 100 caracteres!")]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirme a sua nova Senha")]
        [Compare("NewPassword",ErrorMessage = "As senhas devem ser iguais!")]
        public string ConfirmNewPassword { get; set; }

        public string StatusMessage { get; set; }
    }
}