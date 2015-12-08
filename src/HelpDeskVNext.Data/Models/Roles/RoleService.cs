using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;

namespace HelpDeskVNext.Data.Models.Roles
{
    public class RoleService : ServiceBase, IService<IdentityRole, string>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public RoleService(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IEnumerable<IdentityRole> Get()
        {
            return _applicationDbContext.Roles.Include(x => x.Users);
        }

        public IEnumerable<SelectListItem> GetList(string id = null)
        {
            return Get().Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Name,
                Selected = x.Id == id
            });
        }

        public void Delete(string id)
        {
            var identity = Get(id);
            _applicationDbContext.Roles.Remove(identity);
            SaveChanges();
        }

        public void Create(IdentityRole ticket)
        {
            _applicationDbContext.Roles.Add(ticket);
            SaveChanges();
        }

        public IdentityRole Get(string id)
        {
            return _applicationDbContext.Roles.FirstOrDefault(x => x.Id == id);
        }

        public void Update(IdentityRole ticket)
        {
            _applicationDbContext.Roles.Update(ticket);
            SaveChanges();
        }
    }
}