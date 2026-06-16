using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirplaneTicketsApp.Data;
using AirplaneTicketsApp.Data.Models;

namespace AirplaneTicketsApp.Controllers
{
     public class FlightController
     {
            private AirplaneTicketsContext _context;

            public FlightController(AirplaneTicketsContext context)
            {
                _context = context;
            }

            public List<Flight> GetAll()
            {
                return _context.Flights.ToList();
            }

            public Flight GetById(int id)
            {
                return _context.Flights.FirstOrDefault(f => f.FlightId == id);
            }

            public void Create(Flight flight)
            {
                _context.Flights.Add(flight);
                _context.SaveChanges();
            }

            public void Update(Flight newFlight)
            {
                var currentFlight = _context.Flights.Find(newFlight.FlightId);

                if (currentFlight != null)
                {
                    _context.Entry(currentFlight).CurrentValues.SetValues(newFlight);
                    _context.SaveChanges();
                }
            }

            public void Delete(int id)
            {
                var flight = _context.Flights.Find(id);

                if (flight != null)
                {
                    _context.Flights.Remove(flight);
                    _context.SaveChanges();
                }
            }
        
     }
}
