namespace CarreraCaballos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixPosiciones : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Posicions", "Caballo_Id", "dbo.Caballoes");
            DropForeignKey("dbo.Posicions", "Carrera_Id", "dbo.Carreras");
            DropIndex("dbo.Posicions", new[] { "Caballo_Id" });
            DropIndex("dbo.Posicions", new[] { "Carrera_Id" });
            RenameColumn(table: "dbo.Posicions", name: "Caballo_Id", newName: "CaballoId");
            RenameColumn(table: "dbo.Posicions", name: "Carrera_Id", newName: "CarreraId");
            AlterColumn("dbo.Posicions", "CaballoId", c => c.Long(nullable: false));
            AlterColumn("dbo.Posicions", "CarreraId", c => c.Long(nullable: false));
            CreateIndex("dbo.Posicions", "CarreraId");
            CreateIndex("dbo.Posicions", "CaballoId");
            AddForeignKey("dbo.Posicions", "CaballoId", "dbo.Caballoes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Posicions", "CarreraId", "dbo.Carreras", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posicions", "CarreraId", "dbo.Carreras");
            DropForeignKey("dbo.Posicions", "CaballoId", "dbo.Caballoes");
            DropIndex("dbo.Posicions", new[] { "CaballoId" });
            DropIndex("dbo.Posicions", new[] { "CarreraId" });
            AlterColumn("dbo.Posicions", "CarreraId", c => c.Long());
            AlterColumn("dbo.Posicions", "CaballoId", c => c.Long());
            RenameColumn(table: "dbo.Posicions", name: "CarreraId", newName: "Carrera_Id");
            RenameColumn(table: "dbo.Posicions", name: "CaballoId", newName: "Caballo_Id");
            CreateIndex("dbo.Posicions", "Carrera_Id");
            CreateIndex("dbo.Posicions", "Caballo_Id");
            AddForeignKey("dbo.Posicions", "Carrera_Id", "dbo.Carreras", "Id");
            AddForeignKey("dbo.Posicions", "Caballo_Id", "dbo.Caballoes", "Id");
        }
    }
}
