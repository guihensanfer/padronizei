using Microsoft.EntityFrameworkCore;

namespace Padronizei.Models
{    
    public class AplicacaoDbContext : DbContext
    {
        public AplicacaoDbContext(DbContextOptions<AplicacaoDbContext> options) : base(options)
        {

        }
        public DbSet<Colaborador> Colaboradores{get;set;}
        public DbSet<Conteudo> Conteudos{get;set;}
        public DbSet<Departamento> Departamentos{get;set;}
        public DbSet<GrupoTrabalho> GruposTrabalhos{get;set;}
        public DbSet<Organizacao> Organizacoes{get;set;}
    }
}