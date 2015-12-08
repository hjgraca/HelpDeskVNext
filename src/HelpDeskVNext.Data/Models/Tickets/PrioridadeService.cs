using System.Collections.Generic;
using System.Linq;
using HelpDeskVNext.Data.Entitidades;
using Microsoft.AspNet.Mvc.Rendering;

namespace HelpDeskVNext.Data.Models.Tickets
{
    public class PrioridadeService : ServiceBase, IService<Prioridade, int>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PrioridadeService(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Prioridade Get(int id)
        {
            return _applicationDbContext.Prioridades.FirstOrDefault(x => x.PrioridadeId == id);
        }

        public void Update(Prioridade prioridade)
        {
            _applicationDbContext.Prioridades.Update(prioridade);
            SaveChanges();
        }

        public void Delete(int id)
        {
            var prioridade = Get(id);
            _applicationDbContext.Prioridades.Remove(prioridade);
            SaveChanges();
        }

        public IEnumerable<Prioridade> Get()
        {
            return _applicationDbContext.Prioridades;
        }

        public IEnumerable<SelectListItem> GetList(int id = 0)
        {
            return new List<SelectListItem>
                {
                    new SelectListItem()
                }.Union(Get().Select(x => new SelectListItem
                {
                    Text = x.Designacao,
                    Value = x.PrioridadeId.ToString(),
                    Selected = x.PrioridadeId == id
                }));
        }

        public void Create(Prioridade prioridade)
        {
            _applicationDbContext.Prioridades.Add(prioridade);
            SaveChanges();
        }
    }
}