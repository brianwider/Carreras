namespace CarreraCaballos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixCaballo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Caballoes", "Descripcion", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Caballoes", "Descripcion", c => c.Long(nullable: false));
        }
    }
}
