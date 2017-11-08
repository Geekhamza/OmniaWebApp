namespace TestGUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Equipements",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        reference = c.String(),
                        ProjectRefId = c.Long(nullable: false),
                        classFibre = c.String(),
                        typeFibre = c.String(),
                        nbrFibre = c.Int(),
                        structureCable = c.String(),
                        longeurCable = c.Double(),
                        Type = c.String(),
                        longeur = c.Double(),
                        diametre = c.Double(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Projects", t => t.ProjectRefId, cascadeDelete: true)
                .Index(t => t.ProjectRefId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Zone = c.String(),
                        Country = c.String(),
                        Client = c.String(),
                        Users = c.String(),
                        datDebut = c.DateTime(nullable: false),
                        datFin = c.DateTime(nullable: false),
                        datFinEstime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        price = c.Double(nullable: false),
                        priceHT = c.Double(nullable: false),
                        EquipementREfId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Equipements", t => t.EquipementREfId, cascadeDelete: true)
                .Index(t => t.EquipementREfId);
            
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ServiceREfId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Services", t => t.ServiceREfId, cascadeDelete: true)
                .Index(t => t.ServiceREfId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tasks", "ServiceREfId", "dbo.Services");
            DropForeignKey("dbo.Services", "EquipementREfId", "dbo.Equipements");
            DropForeignKey("dbo.Equipements", "ProjectRefId", "dbo.Projects");
            DropIndex("dbo.Tasks", new[] { "ServiceREfId" });
            DropIndex("dbo.Services", new[] { "EquipementREfId" });
            DropIndex("dbo.Equipements", new[] { "ProjectRefId" });
            DropTable("dbo.Tasks");
            DropTable("dbo.Services");
            DropTable("dbo.Projects");
            DropTable("dbo.Equipements");
        }
    }
}
