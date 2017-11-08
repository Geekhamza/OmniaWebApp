namespace TestGUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Distctionofpehdinoutinmanholeclass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipements", "Manhole_Id2", c => c.Long());
            CreateIndex("dbo.Equipements", "Manhole_Id2");
            AddForeignKey("dbo.Equipements", "Manhole_Id2", "dbo.Equipements", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Equipements", "Manhole_Id2", "dbo.Equipements");
            DropIndex("dbo.Equipements", new[] { "Manhole_Id2" });
            DropColumn("dbo.Equipements", "Manhole_Id2");
        }
    }
}
