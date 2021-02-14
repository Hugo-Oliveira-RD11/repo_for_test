using System;
using System.ComponentModel.DataAnnotations;

namespace test1.Models
{
    public class Usuarios
    {
        [Key]
        public int id{get;set;}
        [Required]
        public string nome{get;set;}
        [Required]
        public string email{get;set;}
        [Required]
        public string senha{get;set;}
    }
}