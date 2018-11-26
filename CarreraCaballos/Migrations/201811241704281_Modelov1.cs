namespace CarreraCaballos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Modelov1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Apuestas",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Valor = c.Single(nullable: false),
                        Caballo_Id = c.Long(),
                        Usuario_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Caballoes", t => t.Caballo_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Usuario_Id)
                .Index(t => t.Caballo_Id)
                .Index(t => t.Usuario_Id);
            
            CreateTable(
                "dbo.Caballoes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Descripcion = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Carreras",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Inicio = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Posicions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Numero = c.Short(nullable: false),
                        Caballo_Id = c.Long(),
                        Carrera_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Caballoes", t => t.Caballo_Id)
                .ForeignKey("dbo.Carreras", t => t.Carrera_Id)
                .Index(t => t.Caballo_Id)
                .Index(t => t.Carrera_Id);
            
            CreateTable(
                "dbo.CarreraCaballoes",
                c => new
                    {
                        Carrera_Id = c.Long(nullable: false),
                        Caballo_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Carrera_Id, t.Caballo_Id })
                .ForeignKey("dbo.Carreras", t => t.Carrera_Id, cascadeDelete: true)
                .ForeignKey("dbo.Caballoes", t => t.Caballo_Id, cascadeDelete: true)
                .Index(t => t.Carrera_Id)
                .Index(t => t.Caballo_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Apuestas", "Usuario_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Apuestas", "Caballo_Id", "dbo.Caballoes");
            DropForeignKey("dbo.Posicions", "Carrera_Id", "dbo.Carreras");
            DropForeignKey("dbo.Posicions", "Caballo_Id", "dbo.Caballoes");
            DropForeignKey("dbo.CarreraCaballoes", "Caballo_Id", "dbo.Caballoes");
            DropForeignKey("dbo.CarreraCaballoes", "Carrera_Id", "dbo.Carreras");
            DropIndex("dbo.CarreraCaballoes", new[] { "Caballo_Id" });
            DropIndex("dbo.CarreraCaballoes", new[] { "Carrera_Id" });
            DropIndex("dbo.Posicions", new[] { "Carrera_Id" });
            DropIndex("dbo.Posicions", new[] { "Caballo_Id" });
            DropIndex("dbo.Apuestas", new[] { "Usuario_Id" });
            DropIndex("dbo.Apuestas", new[] { "Caballo_Id" });
            DropTable("dbo.CarreraCaballoes");
            DropTable("dbo.Posicions");
            DropTable("dbo.Carreras");
            DropTable("dbo.Caballoes");
            DropTable("dbo.Apuestas");
        }
    }
}
