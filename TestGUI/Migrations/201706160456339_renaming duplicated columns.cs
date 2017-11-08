namespace TestGUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renamingduplicatedcolumns : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Equipements", name: "IdPEHD", newName: "PEHDRefId");
            RenameIndex(table: "dbo.Equipements", name: "IX_IdPEHD", newName: "IX_PEHDRefId");
            AddColumn("dbo.Equipements", "typeTube", c => c.Int());
            AddColumn("dbo.Equipements", "DiametreTube", c => c.Double());
            AddColumn("dbo.Equipements", "longeurTube", c => c.Double());
            AddColumn("dbo.Equipements", "PressionPEHD", c => c.Double());
            AddColumn("dbo.Equipements", "DiametrePEHD", c => c.Double());
            AddColumn("dbo.Equipements", "longeurPEHD", c => c.Double());
            AddColumn("dbo.Equipements", "latManhole", c => c.Double());
            AddColumn("dbo.Equipements", "lngManhole", c => c.Double());
            AddColumn("dbo.Equipements", "typeJoint", c => c.String());
            DropColumn("dbo.Equipements", "type");
            DropColumn("dbo.Equipements", "Diametre");
            DropColumn("dbo.Equipements", "longeur");
            DropColumn("dbo.Equipements", "Pression");
            DropColumn("dbo.Equipements", "Diametre1");
            DropColumn("dbo.Equipements", "longeur1");
            DropColumn("dbo.Equipements", "lat");
            DropColumn("dbo.Equipements", "lng");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Equipements", "lng", c => c.Double());
            AddColumn("dbo.Equipements", "lat", c => c.Double());
            AddColumn("dbo.Equipements", "longeur1", c => c.Double());
            AddColumn("dbo.Equipements", "Diametre1", c => c.Double());
            AddColumn("dbo.Equipements", "Pression", c => c.Double());
            AddColumn("dbo.Equipements", "longeur", c => c.Double());
            AddColumn("dbo.Equipements", "Diametre", c => c.Double());
            AddColumn("dbo.Equipements", "type", c => c.Int());
            DropColumn("dbo.Equipements", "typeJoint");
            DropColumn("dbo.Equipements", "lngManhole");
            DropColumn("dbo.Equipements", "latManhole");
            DropColumn("dbo.Equipements", "longeurPEHD");
            DropColumn("dbo.Equipements", "DiametrePEHD");
            DropColumn("dbo.Equipements", "PressionPEHD");
            DropColumn("dbo.Equipements", "longeurTube");
            DropColumn("dbo.Equipements", "DiametreTube");
            DropColumn("dbo.Equipements", "typeTube");
            RenameIndex(table: "dbo.Equipements", name: "IX_PEHDRefId", newName: "IX_IdPEHD");
            RenameColumn(table: "dbo.Equipements", name: "PEHDRefId", newName: "IdPEHD");
        }
    }
}
