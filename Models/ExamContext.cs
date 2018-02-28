using Microsoft.EntityFrameworkCore;


namespace exam2.Models
{
    public class ExamContext : DbContext

    {
        public DbSet<User> User { get; set; }

        public DbSet<Auction> Auction { get; set; }

        public DbSet<UserhasAuction> User_has_Auction { get; set; }
        // base() calls the parent class' constructor passing the "options" parameter along
        public ExamContext(DbContextOptions<ExamContext> options) : base(options) { }
    }
}