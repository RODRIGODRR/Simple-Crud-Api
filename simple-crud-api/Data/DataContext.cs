using Microsoft.EntityFrameworkCore;
using simple_crud_api.Models;

namespace simple_crud_api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder mBuilder)
        {
            // detalhes das propriedades dessa base de dados (tabela 'Users')
            mBuilder.Entity<User>().Property(e => e.Id).HasMaxLength(24).HasColumnType("varchar(24)");
            mBuilder.Entity<User>().Property(e => e.Name).HasMaxLength(100).HasColumnType("varchar(100)");
            mBuilder.Entity<User>().Property(e => e.Email).HasMaxLength(50).HasColumnType("varchar(50)");
        }
    }
}
