namespace Server.Models.Users
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Primeiro Nome")]
        [MaxLength(15),MinLength(3)]
        public string? FirstName { get; set; } 
        
        [Required]
        [Display(Name = "Ultimo Nome")]
        [MaxLength(40),MinLength(2)]
        public string? LastName { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "yyyy/mm/dd")]
        public DateTime DateCreate { get; set; }

        
        [Required]
        [DisplayFormat(DataFormatString = "yyyy/mm/dd")]
        public DateTime LastModified { get; set; }
        
        [Required]
        public string? Role { get; set; }
        
        public List<Post>? Post { get; set; }
    }
}