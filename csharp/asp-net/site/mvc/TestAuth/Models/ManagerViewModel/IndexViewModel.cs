using System;
using System.ComponentModel.DataAnnotations;

namespace TestAuth.Models.ManagerViewModel{
    public class IndexViewModel{
        public string Username { get; set; }
        public bool IsEmailConfirmed { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        [Display(Name = "Telefone")]
        public string Phone { get; set; }
        public string StatusMessege { get; set; }
    }
}