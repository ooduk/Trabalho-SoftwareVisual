using Microsoft.EntityFrameworkCore;
using ApiBackend.Models;

namespace ApiBackend.Data;

public class AppDataContext : DbContext
{
    public AppDataContext (DbContextOptions<AppDataContext> options) : base (options)
    {
        
    }
    // Classes que vão se tornar tabelas do banco
    public DbSet<Campeonato> Campeonatos { get; set; }

    public DbSet<Time> Times { get; set; }

}