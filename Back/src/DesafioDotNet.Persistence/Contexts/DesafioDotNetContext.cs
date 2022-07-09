using Microsoft.EntityFrameworkCore;
using DesafioDotNet.Domain;

namespace DesafioDotNet.Persistence.Contexts
{
    public class DesafioDotNetContext: DbContext
    {
        public DesafioDotNetContext(DbContextOptions<DesafioDotNetContext> options): base(options){ }
        public DbSet<Product> Products { get; set; }         
    }
}