using System.Collections.Generic;
using HelpDeskVNext.Data.Entitidades;

namespace HelpDeskVNext.ViewModels.Tickets
{
    public class BoardColumn
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual IEnumerable<Ticket> Tickets { get; set; }
    }
}
