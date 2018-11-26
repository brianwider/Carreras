namespace CarreraCaballos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixFk : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Apuestas", "Caballo_Id", "dbo.Caballoes");
            DropForeignKey("dbo.Apuestas", "Carrera_Id", "dbo.Carreras");
            DropIndex("dbo.Apuestas", new[] { "Caballo_Id" });
            DropIndex("dbo.Apuestas", new[] { "Carrera_Id" });
            RenameColumn(table: "dbo.Apuestas", name: "Caballo_Id", newName: "CaballoId");
            RenameColumn(table: "dbo.Apuestas", name: "Carrera_Id", newName: "CarreraId");
            RenameColumn(table: "dbo.Apuestas", name: "Usuario_Id", newName: "UsuarioId");
            RenameIndex(table: "dbo.Apuestas", name: "IX_Usuario_Id", newName: "IX_UsuarioId");
            AlterColumn("dbo.Apuestas", "CaballoId", c => c.Long(nullable: false));
            AlterColumn("dbo.Apuestas", "CarreraId", c => c.Long(nullable: false));
            CreateIndex("dbo.Apuestas", "CaballoId");
            CreateIndex("dbo.Apuestas", "CarreraId");
            AddForeignKey("dbo.Apuestas", "CaballoId", "dbo.Caballoes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Apuestas", "CarreraId", "dbo.Carreras", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Apuestas", "CarreraId", "dbo.Carreras");
            DropForeignKey("dbo.Apuestas", "CaballoId", "dbo.Caballoes");
            DropIndex("dbo.Apuestas", new[] { "CarreraId" });
            DropIndex("dbo.Apuestas", new[] { "CaballoId" });
            AlterColumn("dbo.Apuestas", "CarreraId", c => c.Long());
            AlterColumn("dbo.Apuestas", "CaballoId", c => c.Long());
            RenameIndex(table: "dbo.Apuestas", name: "IX_UsuarioId", newName: "IX_Usuario_Id");
            RenameColumn(table: "dbo.Apuestas", name: "UsuarioId", newName: "Usuario_Id");
            RenameColumn(table: "dbo.Apuestas", name: "CarreraId", newName: "Carrera_Id");
            RenameColumn(table: "dbo.Apuestas", name: "CaballoId", newName: "Caballo_Id");
            CreateIndex("dbo.Apuestas", "Carrera_Id");
            CreateIndex("dbo.Apuestas", "Caballo_Id");
            AddForeignKey("dbo.Apuestas", "Carrera_Id", "dbo.Carreras", "Id");
            AddForeignKey("dbo.Apuestas", "Caballo_Id", "dbo.Caballoes", "Id");
        }
    }
}
