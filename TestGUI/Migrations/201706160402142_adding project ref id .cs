namespace TestGUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addingprojectrefid : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Equipements", "Project_Id", "dbo.Projects");
            DropIndex("dbo.Equipements", new[] { "Project_Id" });
            RenameColumn(table: "dbo.Equipements", name: "Project_Id", newName: "ProjectRefId");
            AlterColumn("dbo.Equipements", "Type", c => c.String());
            AlterColumn("dbo.Equipements", "type", c => c.Int());
            AlterColumn("dbo.Equipements", "ProjectRefId", c => c.Long(nullable: false));
            CreateIndex("dbo.Equipements", "ProjectRefId");
            AddForeignKey("dbo.Equipements", "ProjectRefId", "dbo.Projects", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Equipements", "ProjectRefId", "dbo.Projects");
            DropIndex("dbo.Equipements", new[] { "ProjectRefId" });
            AlterColumn("dbo.Equipements", "ProjectRefId", c => c.Long());
            AlterColumn("dbo.Equipements", "type", c => c.String());
            AlterColumn("dbo.Equipements", "Type", c => c.Int());
            RenameColumn(table: "dbo.Equipements", name: "ProjectRefId", newName: "Project_Id");
            CreateIndex("dbo.Equipements", "Project_Id");
            AddForeignKey("dbo.Equipements", "Project_Id", "dbo.Projects", "Id");
        }
    }
}
