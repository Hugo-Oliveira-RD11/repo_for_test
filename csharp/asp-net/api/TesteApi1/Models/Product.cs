using System;
using System.ComponentModel.DataAnnotations;

namespace TesteApi1.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Voce tem quer ter um titulo!")]
        [StringLength(40,MinimumLength = 5)]
        public string Title { get; set; }
        [StringLength(4000)]
        public string  Description { get; set; }
        [Required]
        [Range(1,int.MaxValue)]
        public decimal Price { get; set; }
        public Category Categors { get; set; }
    }

}