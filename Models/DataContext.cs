using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DersYonetimSistemi.Models;

public class DataContext : IdentityDbContext<AppUser, AppRole, int>
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Ogrenci> Ogrenciler { get; set; }
    public DbSet<Ogretmen> Ogretmenler { get; set; }
    public DbSet<Ders> Dersler { get; set; }
    public DbSet<Hafta> Haftalar { get; set; }
    public DbSet<DosyaKaynagi> DosyaKaynaklari { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    }
}