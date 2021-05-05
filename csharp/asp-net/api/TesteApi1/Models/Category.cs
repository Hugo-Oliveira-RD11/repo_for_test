using System;
using System.ComponentModel.DataAnnotations;

namespace TesteApi1.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [StringLength(40,MinimumLength = 4)]
        public string Title { get; set; }


    }
}