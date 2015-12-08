using System.Collections.Generic;
using System.Linq;
using HelpDeskVNext.Data.Entitidades;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;

namespace HelpDeskVNext.Data.Models.Tickets
{
    public class TicketService : ServiceBase, IService<Ticket, int>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public TicketService(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Ticket Get(int id)
        {
            return _applicationDbContext.Tickets.FirstOrDefault(x => x.TicketId == id);
        }

        public void Update(Ticket ticket)
        {
            _applicationDbContext.Tickets.Update(ticket);
            SaveChanges();
        }

        public void Delete(int id)
        {
            var ticket = Get(id);
            _applicationDbContext.Tickets.Remove(ticket);
            SaveChanges();
        }

        public IEnumerable<Ticket> Get()
        {
            return _applicationDbContext.Tickets.Include(x => x.Prioridade);
        }

        public IEnumerable<SelectListItem> GetList(int id = 0)
        {
            throw new System.NotImplementedException();
        }

        public void Create(Ticket ticket)
        {
            _applicationDbContext.Tickets.Add(ticket);
            SaveChanges();
        }
    }
}