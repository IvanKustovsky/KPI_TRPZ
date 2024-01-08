using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF
{
    public class SanatoriumDbContext : DbContext
    {
        public DbSet<Sanatorium> Sanatoriums { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public SanatoriumDbContext(DbContextOptions options) : base(options) { }
    }
}
