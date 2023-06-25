using Microsoft.EntityFrameworkCore;

namespace DinamicaApi.Models;

public class DinamicaContext : DbContext
{
    public DinamicaContext(DbContextOptions<DinamicaContext> options)
        : base(options)
    {
    }

    public DbSet<TextoEnviado> TextosEnviados { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<TextoEnviado>().HasKey(m => m.Id);
        base.OnModelCreating(builder);
    }
}