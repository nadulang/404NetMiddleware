using Microsoft.EntityFrameworkCore;

namespace ContactWebAPI.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
                    : base(options)
                    {   
                    }

                    public DbSet<UserContact> UserContacts { get; set; }
    }
}