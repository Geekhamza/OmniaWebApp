namespace TestGUI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Equipements", "ProjectRefId", "dbo.Projects");
            DropIndex("dbo.Equipements", new[] { "ProjectRefId" });
            RenameColumn(table: "dbo.Equipements", name: "ProjectRefId", newName: "Project_Id");
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        name = c.String(),
                        ContactPM = c.String(),
                        ContactFinancier = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.RapportPhotoes",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        lat = c.Double(nullable: false),
                        lng = c.Double(nullable: false),
                        photo = c.String(),
                        distance = c.Double(nullable: false),
                        type = c.String(),
                        descrition = c.String(),
                        ServiceRefId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Services", t => t.ServiceRefId, cascadeDelete: true)
                .Index(t => t.ServiceRefId);
            
            CreateTable(
                "dbo.Subcoes",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        login = c.String(),
                        psw = c.String(),
                        Phone = c.String(),
                        Contact = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Progresses",
                c => new
                    {
                        id = c.Long(nullable: false, identity: true),
                        progress = c.Double(nullable: false),
                        date = c.DateTime(nullable: false),
                        TacheRefId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Taches", t => t.TacheRefId, cascadeDelete: true)
                .Index(t => t.TacheRefId);
            
            CreateTable(
                "dbo.Timesheets",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        idUser = c.Long(nullable: false),
                        idtache = c.Long(nullable: false),
                        date = c.DateTime(nullable: false),
                        time = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProjectClients",
                c => new
                    {
                        Project_Id = c.Long(nullable: false),
                        Client_id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Project_Id, t.Client_id })
                .ForeignKey("dbo.Projects", t => t.Project_Id, cascadeDelete: true)
                .ForeignKey("dbo.Clients", t => t.Client_id, cascadeDelete: true)
                .Index(t => t.Project_Id)
                .Index(t => t.Client_id);
            
            CreateTable(
                "dbo.SubcoServices",
                c => new
                    {
                        Subco_id = c.Long(nullable: false),
                        Service_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Subco_id, t.Service_Id })
                .ForeignKey("dbo.Subcoes", t => t.Subco_id, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.Service_Id, cascadeDelete: true)
                .Index(t => t.Subco_id)
                .Index(t => t.Service_Id);
            
            AddColumn("dbo.Equipements", "IdTube", c => c.Long());
            AddColumn("dbo.Equipements", "IdPEHD", c => c.Long());
            AddColumn("dbo.Equipements", "Pression", c => c.Double());
            AddColumn("dbo.Equipements", "Diametre1", c => c.Double());
            AddColumn("dbo.Equipements", "longeur1", c => c.Double());
            AddColumn("dbo.Equipements", "IdManholeSrc", c => c.Long());
            AddColumn("dbo.Equipements", "IdManholeDist", c => c.Long());
            AddColumn("dbo.Equipements", "lat", c => c.Double());
            AddColumn("dbo.Equipements", "lng", c => c.Double());
            AddColumn("dbo.Equipements", "TypeCh", c => c.String());
            AddColumn("dbo.Equipements", "ManholeId", c => c.Long());
            AddColumn("dbo.Equipements", "Manhole_Id", c => c.Long());
            AddColumn("dbo.Equipements", "Manhole_Id1", c => c.Long());
            AddColumn("dbo.Taches", "Nom", c => c.String());
            AddColumn("dbo.Taches", "AffectedUSers", c => c.String());
            AddColumn("dbo.Taches", "dureEstime", c => c.Int(nullable: false));
            AddColumn("dbo.Taches", "dureeReel", c => c.Int());
            AddColumn("dbo.Taches", "dateDepart", c => c.DateTime(nullable: false));
            AddColumn("dbo.Taches", "dateFin", c => c.DateTime(nullable: false));
            AddColumn("dbo.Taches", "Description", c => c.String());
            AlterColumn("dbo.Equipements", "Project_Id", c => c.Long());
            AlterColumn("dbo.Equipements", "type", c => c.Int());
            CreateIndex("dbo.Equipements", "IdTube");
            CreateIndex("dbo.Equipements", "IdPEHD");
            CreateIndex("dbo.Equipements", "IdManholeSrc");
            CreateIndex("dbo.Equipements", "IdManholeDist");
            CreateIndex("dbo.Equipements", "ManholeId");
            CreateIndex("dbo.Equipements", "Manhole_Id");
            CreateIndex("dbo.Equipements", "Manhole_Id1");
            CreateIndex("dbo.Equipements", "Project_Id");
            AddForeignKey("dbo.Equipements", "IdTube", "dbo.Equipements", "Id");
            AddForeignKey("dbo.Equipements", "Manhole_Id", "dbo.Equipements", "Id");
            AddForeignKey("dbo.Equipements", "Manhole_Id1", "dbo.Equipements", "Id");
            AddForeignKey("dbo.Equipements", "IdManholeDist", "dbo.Equipements", "Id");
            AddForeignKey("dbo.Equipements", "IdManholeSrc", "dbo.Equipements", "Id");
            AddForeignKey("dbo.Equipements", "IdPEHD", "dbo.Equipements", "Id");
            AddForeignKey("dbo.Equipements", "ManholeId", "dbo.Equipements", "Id");
            AddForeignKey("dbo.Equipements", "Project_Id", "dbo.Projects", "Id");
            DropColumn("dbo.Projects", "Client");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Projects", "Client", c => c.String());
            DropForeignKey("dbo.Equipements", "Project_Id", "dbo.Projects");
            DropForeignKey("dbo.Equipements", "ManholeId", "dbo.Equipements");
            DropForeignKey("dbo.Equipements", "IdPEHD", "dbo.Equipements");
            DropForeignKey("dbo.Equipements", "IdManholeSrc", "dbo.Equipements");
            DropForeignKey("dbo.Equipements", "IdManholeDist", "dbo.Equipements");
            DropForeignKey("dbo.Equipements", "Manhole_Id1", "dbo.Equipements");
            DropForeignKey("dbo.Equipements", "Manhole_Id", "dbo.Equipements");
            DropForeignKey("dbo.Equipements", "IdTube", "dbo.Equipements");
            DropForeignKey("dbo.Progresses", "TacheRefId", "dbo.Taches");
            DropForeignKey("dbo.SubcoServices", "Service_Id", "dbo.Services");
            DropForeignKey("dbo.SubcoServices", "Subco_id", "dbo.Subcoes");
            DropForeignKey("dbo.RapportPhotoes", "ServiceRefId", "dbo.Services");
            DropForeignKey("dbo.ProjectClients", "Client_id", "dbo.Clients");
            DropForeignKey("dbo.ProjectClients", "Project_Id", "dbo.Projects");
            DropIndex("dbo.SubcoServices", new[] { "Service_Id" });
            DropIndex("dbo.SubcoServices", new[] { "Subco_id" });
            DropIndex("dbo.ProjectClients", new[] { "Client_id" });
            DropIndex("dbo.ProjectClients", new[] { "Project_Id" });
            DropIndex("dbo.Progresses", new[] { "TacheRefId" });
            DropIndex("dbo.RapportPhotoes", new[] { "ServiceRefId" });
            DropIndex("dbo.Equipements", new[] { "Project_Id" });
            DropIndex("dbo.Equipements", new[] { "Manhole_Id1" });
            DropIndex("dbo.Equipements", new[] { "Manhole_Id" });
            DropIndex("dbo.Equipements", new[] { "ManholeId" });
            DropIndex("dbo.Equipements", new[] { "IdManholeDist" });
            DropIndex("dbo.Equipements", new[] { "IdManholeSrc" });
            DropIndex("dbo.Equipements", new[] { "IdPEHD" });
            DropIndex("dbo.Equipements", new[] { "IdTube" });
            AlterColumn("dbo.Equipements", "type", c => c.String());
            AlterColumn("dbo.Equipements", "Project_Id", c => c.Long(nullable: false));
            DropColumn("dbo.Taches", "Description");
            DropColumn("dbo.Taches", "dateFin");
            DropColumn("dbo.Taches", "dateDepart");
            DropColumn("dbo.Taches", "dureeReel");
            DropColumn("dbo.Taches", "dureEstime");
            DropColumn("dbo.Taches", "AffectedUSers");
            DropColumn("dbo.Taches", "Nom");
            DropColumn("dbo.Equipements", "Manhole_Id1");
            DropColumn("dbo.Equipements", "Manhole_Id");
            DropColumn("dbo.Equipements", "ManholeId");
            DropColumn("dbo.Equipements", "TypeCh");
            DropColumn("dbo.Equipements", "lng");
            DropColumn("dbo.Equipements", "lat");
            DropColumn("dbo.Equipements", "IdManholeDist");
            DropColumn("dbo.Equipements", "IdManholeSrc");
            DropColumn("dbo.Equipements", "longeur1");
            DropColumn("dbo.Equipements", "Diametre1");
            DropColumn("dbo.Equipements", "Pression");
            DropColumn("dbo.Equipements", "IdPEHD");
            DropColumn("dbo.Equipements", "IdTube");
            DropTable("dbo.SubcoServices");
            DropTable("dbo.ProjectClients");
            DropTable("dbo.Timesheets");
            DropTable("dbo.Progresses");
            DropTable("dbo.Subcoes");
            DropTable("dbo.RapportPhotoes");
            DropTable("dbo.Clients");
            RenameColumn(table: "dbo.Equipements", name: "Project_Id", newName: "ProjectRefId");
            CreateIndex("dbo.Equipements", "ProjectRefId");
            AddForeignKey("dbo.Equipements", "ProjectRefId", "dbo.Projects", "Id", cascadeDelete: true);
        }
    }
}
