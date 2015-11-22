using System.Collections.Generic;
using System.Linq;
using HelpDeskVNext.Data.Entitidades;
using Microsoft.AspNet.Mvc.Rendering;

namespace HelpDeskVNext.Data.Models.Departamentos
{
    public class DepartamentoService : ServiceBase, IService<Departamento, int>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IEnumerable<SelectListItem> GetList(int id = 0)
        {
            return new List<SelectListItem>
                {
                    new SelectListItem()
                }.Union(Get().Select(x => new SelectListItem
                {
                    Text = x.Nome,
                    Value = x.DepartamentoId.ToString(),
                    Selected = x.DepartamentoId == id
                }));
        }

        public DepartamentoService(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Departamento Get(int id)
        {
            return _applicationDbContext.Departamentos.FirstOrDefault(x => x.DepartamentoId == id);
        }

        public void Update(Departamento departamento)
        {
            _applicationDbContext.Departamentos.Update(departamento);
            SaveChanges();
        }

        public void Delete(int id)
        {
            var departamento = Get(id);
            _applicationDbContext.Departamentos.Remove(departamento);
            SaveChanges();
        }

        public IEnumerable<Departamento> Get()
        {
            return _applicationDbContext.Departamentos;
        }

        public void Create(Departamento departamento)
        {
            _applicationDbContext.Departamentos.Add(departamento);
            SaveChanges();
        }
    }
}