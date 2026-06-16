using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirplaneTicketsApp.Data;
using AirplaneTicketsApp.Data.Models;

namespace AirplaneTicketsApp.Controllers
{
    public class TicketController
    {
            private AirplaneTicketsContext _context;

            public TicketController(AirplaneTicketsContext context)
            {
                _context = context;
            }

            public List<Ticket> GetAll()
            {
                return _context.Tickets.ToList();
            }

            public Ticket GetById(int id)
            {
                return _context.Tickets.FirstOrDefault(t => t.TicketId == id);
            }

            public void Create(Ticket ticket)
            {
                _context.Tickets.Add(ticket);
                _context.SaveChanges();
            }

            public void Update(Ticket newTicket)
            {
                var currentTicket = _context.Tickets.Find(newTicket.TicketId);

                if (currentTicket != null)
                {
                    _context.Entry(currentTicket).CurrentValues.SetValues(newTicket);
                    _context.SaveChanges();
                }
            }

            public void Delete(int id)
            {
                var ticket = _context.Tickets.Find(id);

                if (ticket != null)
                {
                    _context.Tickets.Remove(ticket);
                    _context.SaveChanges();
                }
            }
    }
}
