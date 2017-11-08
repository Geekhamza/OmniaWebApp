using MyDomain.Entities;
using System;
using System.Data.Entity;


namespace MyData
{
    public class MyContext : DbContext
    {
        public MyContext()
            : base("Data Source=.;Initial Catalog=omnia_db;Integrated Security=True") //na3tih il bd "data source=. ya3ni ilocal " initial catalog = nom de bd" integrated security = paramétrage de securité
        {
            Database.SetInitializer<MyContext>(new MyContextInitializer()); // ki n'initializi il bd n9olou imchi il  MyContextInitializer() illi hya illouta
            //changed this to ingnore cheking database changment on start
           // Database.SetInitializer<MyContext>(null);
           // Configuration.LazyLoadingEnabled = false;

        }
        public virtual void Commit()
        {
            base.SaveChanges(); // ki nmodifi tvalidi fil bd
        }
        public DbSet<Project> projects { get; set; } // a3mali table essmha projects  lil entité li fil crochet
        public DbSet<Equipement> equipements { get; set; }
        public DbSet<Service> services { get; set; }
        public DbSet<Tache> tasks { get; set; }
        public DbSet<Client> clients { get; set; }
        public DbSet<Progress> progresses { get; set; }
        public DbSet<Subco> subcos { get; set; }
        public DbSet<Timesheet> timesheets { get; set; }
        public DbSet<RapportPhoto> rapportphoto { get; set; }
        public DbSet<TemplateEquipement> templateEquipement { get; set; }
        public DbSet<TemplateService> templateService { get; set; }
        public DbSet<TemplateTache> templateTache { get; set; }
    }
    public class MyContextInitializer : CreateDatabaseIfNotExists<MyContext>
    {
        protected override void Seed(MyContext context)
        {
        }
    }

}


