using HelpDeskVNext.Data.Entitidades;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HelpDeskVNext.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public virtual Departamento Departamento { get; set; }
        public string Nome { get; set; }
    }
}
