namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TbEmpresas",
                c => new
                    {
                        EmpresaID = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Codigo = c.Int(nullable: false),
                        Direccion = c.String(maxLength: 80),
                        Telefono = c.String(maxLength: 10),
                        Ciudad = c.String(maxLength: 50),
                        Departamento = c.String(maxLength: 50),
                        Pais = c.String(maxLength: 50),
                        FechaCreacion = c.DateTime(),
                        FechaModificacion = c.DateTime(),
                    })
                .PrimaryKey(t => t.EmpresaID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TbEmpresas");
        }
    }
}
