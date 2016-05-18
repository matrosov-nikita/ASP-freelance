using ExchangeFreelancing.Domain.Entities;
using System.Data.Entity;

namespace ExchangeFreelancing.Domain.Concrete
{
    public class EFDbContext:DbContext
    {
        public EFDbContext():base("DefaultConnection")
        {

        }
       public DbSet<Order> Orders { get; set; }
       public DbSet<Category> Categories { get; set; }
       public DbSet<Request> Requests { get; set; }
       public DbSet<File> Files { get; set; }
       public DbSet<Message> Messages { get; set; }
       public DbSet<Claim> Claims { get; set; }
       public DbSet<Comment> Comments { get; set; }

    }
}
