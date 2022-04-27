using DesafioJuntoSeguros.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace DesafioJuntoSeguros.Domain
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
    }
  }