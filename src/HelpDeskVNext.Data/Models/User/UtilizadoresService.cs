using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;

namespace HelpDeskVNext.Data.Models.User
{
    public class UtilizadoresService : ServiceBase, IService<ApplicationUser, string>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UtilizadoresService(ApplicationDbContext applicationDbContext)
            :base (applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public IEnumerable<SelectListItem> GetList(string id = null)
        {
            return new List<SelectListItem>
                {
                    new SelectListItem()
                }.Union(Get().Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.Id.ToString(),
                    Selected = x.Id == id
                }));
        }

        public void Create(ApplicationUser item)
        {
            _applicationDbContext.Users.Add(item);
            SaveChanges();
        }

        public void Delete(string id)
        {
            var user = Get(id);
            _applicationDbContext.Remove(user);
            SaveChanges();
        }

        public IEnumerable<ApplicationUser> Get()
        {
            return _applicationDbContext.Users.Include(x => x.Roles).Include(x => x.Departamento);
        }

        public ApplicationUser Get(string id)
        {
            return _applicationDbContext.Users.Include(x => x.Roles).Include(x => x.Departamento).FirstOrDefault(x => x.Id == id);
        }

        public void Update(ApplicationUser item)
        {
            var result = _applicationDbContext.Update(item);
            SaveChanges();
        }
    }
}
