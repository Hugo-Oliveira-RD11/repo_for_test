using System.ComponentModel.DataAnnotations;

namespace TestAuth.Models.AccountViewModel
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }
        [Display(Name = "Lembrar de mim ")]
        public bool Rememberme { get; set; }
    }
}