using System.Data.Entity;
using MvcFuncionarios.Models;


namespace MvcFuncionarios.DataContext
{
       public class FuncionariosDataContext : DbContext
    {
           public FuncionariosDataContext()
            : base("funcionariosEntities1")
        {
        }
        public DbSet<Funcionarios> Funcionarios { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
       
    }
}

