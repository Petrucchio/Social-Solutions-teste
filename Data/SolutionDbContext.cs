using Microsoft.EntityFrameworkCore;
using Social_solution_teste.Models;

namespace Social_solution_teste.Data
{
    public class SolutionDbContext : DbContext
    {
        public SolutionDbContext(DbContextOptions<SolutionDbContext> options)
           : base(options)
        {
        }
        //Para futuramente adicionar no banco de dados
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Imovel> Imoveis { get; set; }
    }
}
