namespace CarreraCaballos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixApuestas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Apuestas", "Carrera_Id", c => c.Long());
            CreateIndex("dbo.Apuestas", "Carrera_Id");
            AddForeignKey("dbo.Apuestas", "Carrera_Id", "dbo.Carreras", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Apuestas", "Carrera_Id", "dbo.Carreras");
            DropIndex("dbo.Apuestas", new[] { "Carrera_Id" });
            DropColumn("dbo.Apuestas", "Carrera_Id");
        }
    }
}
