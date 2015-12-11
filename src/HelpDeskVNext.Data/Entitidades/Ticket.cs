using System;
using HelpDeskVNext.Data.Models;

namespace HelpDeskVNext.Data.Entitidades
{
    public class Ticket
    {
        public int TicketId { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataInsercao { get; set; }
        public DateTime? DataConclusao { get; set; }
        public int EstadoId { get; set; }
        public virtual Estado Estado { get; set; }
        public string TecnicoId { get; set; }
        public virtual ApplicationUser Tecnico { get; set; }
        public string CreatedByUtilizadorId { get; set; }
        public virtual ApplicationUser CreatedByUtilizador { get; set; }
        public int PrioridadeId { get; set; }
        public virtual Prioridade Prioridade { get; set; }
        public int? DepartamentoId { get; set; }
        public virtual Departamento Departamento { get; set; }
    }
}
