using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirplaneTicketsApp.Data;
using AirplaneTicketsApp.Data.Models;

namespace AirplaneTicketsApp.Controllers
{
    public class PassengerController
    {
        private AirplaneTicketsContext _context;

        public PassengerController(AirplaneTicketsContext context)
        {
            _context = context;
        }

        public List<Passenger> GetAll()
        {
            return _context.Passengers.ToList();
        }

        public Passenger GetById(int id)
        {
            return _context.Passengers.FirstOrDefault(p => p.PassengerId == id);
        }

        public void Create(Passenger passenger)
        {
            _context.Passengers.Add(passenger);
            _context.SaveChanges();
        }

        public void Update(Passenger newPassenger)
        {
            var currentPassenger = _context.Passengers.Find(newPassenger.PassengerId);

            if (currentPassenger != null)
            {
                _context.Entry(currentPassenger).CurrentValues.SetValues(newPassenger);
                _context.SaveChanges();
            }
        }

        public void Delete(int id)
        {
            var passenger = _context.Passengers.Find(id);

            if (passenger != null)
            {
                _context.Passengers.Remove(passenger);
                _context.SaveChanges();
            }
        }
    }
}
