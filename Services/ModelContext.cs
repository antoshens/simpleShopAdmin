using Microsoft.EntityFrameworkCore;

namespace FoodLavkaAdmin.Models.ModelStorage
{
    public class ModelContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders {get; set;}
        public DbSet<PermissionRole> PermissionRoles { get; set; }
        public DbSet<User> Users { get; set; }

        public ModelContext(DbContextOptions<ModelContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }
    }
}
