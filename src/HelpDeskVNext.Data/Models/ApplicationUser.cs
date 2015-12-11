using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HelpDeskVNext.Data.Entitidades;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HelpDeskVNext.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public int? DepartamentoId { get; set; }
        public virtual Departamento Departamento { get; set; }
        public string Nome { get; set; }
        [NotMapped]
        public IEnumerable<string> RoleNames { get; set; }
    }

    public class IdentityDbContextOptions
    {
        public string DefaultAdminUserName { get; set; }
        public string DefaultAdminPassword { get; set; }
        public string Roles { get; set; }
        public string AdministratorRole { get; set; }
    }
}
