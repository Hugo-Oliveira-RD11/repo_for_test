namespace Server.Models.Posts
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(45),MinLength(5)]
        public string? Title { get; set; }
        public string? Tag { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string? Content { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "yyyy/mm/dd")]
        public DateTime DateCreate { get; set; }
        
        [Required]
        [DisplayFormat(DataFormatString = "yyyy/mm/dd")]
        public DateTime LastModification { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}