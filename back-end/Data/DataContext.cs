using back_end.Models;
using Microsoft.EntityFrameworkCore;
using QualyTeamTest.Models;

namespace QualyTeamTest.Data
{
    public class DataContext: DbContext
    {
        public DbSet<Documento> Documento { get; set; }
        public DbSet<Processo> Processo { get; set; }
        public DbSet<Upload> Upload { get; set; } 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string stringConexao = "server=localhost;DataBase=qualyteamtest;Uid=root;Pwd=Senha123";
            optionsBuilder.UseMySql(stringConexao, ServerVersion.AutoDetect(stringConexao));
        }
    }
}
