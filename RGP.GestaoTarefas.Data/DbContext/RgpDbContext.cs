using Microsoft.EntityFrameworkCore;
using RGP.GestaoTarefas.Domain.Entities;

public class RgpDbContext : DbContext
{
    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }

    public RgpDbContext(DbContextOptions<RgpDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tarefa>()
            .HasOne(t => t.Usuario)
            .WithMany(u => u.Tarefas)
            .HasForeignKey(t => t.UsuarioId);
    }
}
