using Microsoft.EntityFrameworkCore;
using Tarefas_Kanban.Models;

namespace Tarefas_Kanban.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { 
        }
        public DbSet<Tarefas_Kanban.Models.Atividade> Atividade { get; set; } = default!;
        public DbSet<Tarefas_Kanban.Models.Usuario> Usuario { get; set; } = default!;
    }
}
