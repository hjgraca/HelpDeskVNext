using HelpDeskVNext.Data.Entitidades;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;

namespace HelpDeskVNext.Data.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Prioridade> Prioridades { get; set; }
        public DbSet<Estado> Estados { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Conventions.Remove<PluralizingTableNameConvention>();

            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
