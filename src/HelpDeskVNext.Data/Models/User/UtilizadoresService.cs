using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;

namespace HelpDeskVNext.Data.Models.User
{
    public class UtilizadoresService : ServiceBase, IService<ApplicationUser, string>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UtilizadoresService(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IEnumerable<SelectListItem> GetList(string id = null)
        {
            throw new NotImplementedException();
        }

        public void Create(ApplicationUser ticket)
        {
            _applicationDbContext.Users.Add(ticket);
            SaveChanges();
        }

        public void Delete(string id)
        {
            var user = Get(id);
            _applicationDbContext.Users.Remove(user);
            SaveChanges();
        }

        public IEnumerable<ApplicationUser> Get()
        {
            return _applicationDbContext.Users.Include(x => x.Roles).Include(x => x.Departamento);
        }

        public ApplicationUser Get(string id)
        {
            var user = _applicationDbContext.Users.Include(x => x.Roles).Include(x => x.Departamento).FirstOrDefault(x => x.Id == id);
            return user;
        }

        public void Update(ApplicationUser ticket)
        {
            _applicationDbContext.Users.Update(ticket);
            SaveChanges();
        }
    }
}
