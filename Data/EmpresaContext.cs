using System.Data.Entity;

namespace Data
{
    public class EmpresaContext : DbContext
    {
        public EmpresaContext() : base("EmpresaConnection")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<EmpresaContext>());
        }

        public DbSet<TbEmpresa> _Empresa { get; set; }
    }
}
