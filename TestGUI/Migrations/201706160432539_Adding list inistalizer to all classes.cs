namespace TestGUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addinglistinistalizertoallclasses : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Equipements", "Type", c => c.String());
            AlterColumn("dbo.Equipements", "type", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Equipements", "type", c => c.String());
            AlterColumn("dbo.Equipements", "Type", c => c.Int());
        }
    }
}
