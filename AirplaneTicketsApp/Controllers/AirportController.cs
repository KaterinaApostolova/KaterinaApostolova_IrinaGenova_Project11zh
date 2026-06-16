using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirplaneTicketsApp.Data;
using AirplaneTicketsApp.Data.Models;

namespace AirplaneTicketsApp.Controllers
{
    public class AirportController
    {
            private AirplaneTicketsContext _context;

            public AirportController(AirplaneTicketsContext context)
            {
                _context = context;
            }

            public List<Airport> GetAll()
            {
                return _context.Airports.ToList();
            }

            public Airport GetById(int id)
            {
                return _context.Airports.FirstOrDefault(a => a.AirportId == id);
            }

            public void Create(Airport airport)
            {
                _context.Airports.Add(airport);
                _context.SaveChanges();
            }

            public void Update(Airport newAirport)
            {
                var currentAirport = _context.Airports.Find(newAirport.AirportId);

                if (currentAirport != null)
                {
                    _context.Entry(currentAirport).CurrentValues.SetValues(newAirport);
                    _context.SaveChanges();
                }
            }

            public void Delete(int id)
            {
                var airport = _context.Airports.Find(id);

                if (airport != null)
                {
                    _context.Airports.Remove(airport);
                    _context.SaveChanges();
                }
            }
    }
}
