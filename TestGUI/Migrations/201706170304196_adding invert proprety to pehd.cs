namespace TestGUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addinginvertpropretytopehd : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Equipements", "Manhole_Id1", "dbo.Equipements");
            DropIndex("dbo.Equipements", new[] { "Manhole_Id1" });
            DropColumn("dbo.Equipements", "Manhole_Id1");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Equipements", "Manhole_Id1", c => c.Long());
            CreateIndex("dbo.Equipements", "Manhole_Id1");
            AddForeignKey("dbo.Equipements", "Manhole_Id1", "dbo.Equipements", "Id");
        }
    }
}
