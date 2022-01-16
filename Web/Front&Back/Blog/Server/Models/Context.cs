namespace Server.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> op):base(op){}

        public DbSet<User> Users { get; set; } =  null!;
        public DbSet<Post> Posts { get; set; } =  null!;
    }
}