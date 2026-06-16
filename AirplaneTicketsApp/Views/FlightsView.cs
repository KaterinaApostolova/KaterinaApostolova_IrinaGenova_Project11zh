using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirplaneTicketsApp.Controllers;
using AirplaneTicketsApp.Data.Models;

namespace AirplaneTicketsApp.Views
{
    public class FlightsView
    {
            private FlightController _controller;
            private AirportController _airportController;

            public FlightsView(FlightController controller, AirportController airportController)
            {
                _controller = controller;
                _airportController = airportController;
            }

            public void Start()
            {
                int op;
                do
                {
                    Console.WriteLine("\n--- FLIGHTS ---");
                    Console.WriteLine("1. List all");
                    Console.WriteLine("2. Add flight");
                    Console.WriteLine("3. Update flight");
                    Console.WriteLine("4. Get by ID (full details)");
                    Console.WriteLine("5. Delete flight");
                    Console.WriteLine("6. Back");
                    Console.Write("Choice: ");

                    if (!int.TryParse(Console.ReadLine(), out op)) { Console.WriteLine("Invalid input."); continue; }

                    switch (op)
                    {
                        case 1:
                            var all = _controller.GetAll();
                            if (!all.Any()) { Console.WriteLine("No flights found."); break; }
                            Console.WriteLine($"\n{"ID",-5} {"Number",-12} {"Departure",-20} {"Arrival",-20} {"Price",-10} {"From → To"}");
                            Console.WriteLine(new string('-', 90));
                            foreach (var f in all)
                            {
                                var dep = _airportController.GetById(f.DepartureAirportId)?.IATACode ?? "?";
                                var arr = _airportController.GetById(f.ArrivalAirportId)?.IATACode ?? "?";
                                Console.WriteLine($"{f.FlightId,-5} {f.FlightNumber,-12} {f.DepartureTime:yyyy-MM-dd HH:mm,-20} {f.ArrivalTime:yyyy-MM-dd HH:mm,-20} {f.Price,-10:F2} {dep}→{arr}");
                            }
                            break;

                        case 2:
                            var newFlight = new Flight();
                            Console.Write("Flight Number (e.g. FR1234): ");
                            newFlight.FlightNumber = Console.ReadLine();

                            Console.Write("Departure time (yyyy-MM-dd HH:mm): ");
                            if (!DateTime.TryParse(Console.ReadLine(), out DateTime depTime)) { Console.WriteLine("Invalid date."); break; }
                            newFlight.DepartureTime = depTime;

                            Console.Write("Arrival time (yyyy-MM-dd HH:mm): ");
                            if (!DateTime.TryParse(Console.ReadLine(), out DateTime arrTime)) { Console.WriteLine("Invalid date."); break; }
                            newFlight.ArrivalTime = arrTime;

                            Console.Write("Price: ");
                            if (!decimal.TryParse(Console.ReadLine(), out decimal price)) { Console.WriteLine("Invalid price."); break; }
                            newFlight.Price = price;

                            Console.WriteLine("\nAvailable airports:");
                            foreach (var a in _airportController.GetAll())
                                Console.WriteLine($"  [{a.AirportId}] {a.Name} ({a.IATACode}) - {a.City}, {a.Country}");

                            Console.Write("Departure Airport ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int depAirId)) { Console.WriteLine("Invalid ID."); break; }
                            if (_airportController.GetById(depAirId) == null) { Console.WriteLine("Departure airport not found."); break; }
                            newFlight.DepartureAirportId = depAirId;

                            Console.Write("Arrival Airport ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int arrAirId)) { Console.WriteLine("Invalid ID."); break; }
                            if (_airportController.GetById(arrAirId) == null) { Console.WriteLine("Arrival airport not found."); break; }
                            newFlight.ArrivalAirportId = arrAirId;

                            _controller.Create(newFlight);
                            Console.WriteLine("Flight added successfully.");
                            break;

                        case 3:
                            Console.Write("Enter Flight ID to update: ");
                            if (!int.TryParse(Console.ReadLine(), out int updId)) { Console.WriteLine("Invalid ID."); break; }
                            var toUpdate = _controller.GetById(updId);
                            if (toUpdate == null) { Console.WriteLine("Flight not found."); break; }

                            Console.WriteLine($"Current flight number: {toUpdate.FlightNumber}");
                            Console.Write("New flight number (leave blank to keep): ");
                            var newNum = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newNum)) toUpdate.FlightNumber = newNum;

                            Console.WriteLine($"Current price: {toUpdate.Price:F2}");
                            Console.Write("New price (leave blank to keep): ");
                            var priceStr = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(priceStr) && decimal.TryParse(priceStr, out decimal newPrice))
                                toUpdate.Price = newPrice;

                            Console.WriteLine($"Current departure time: {toUpdate.DepartureTime:yyyy-MM-dd HH:mm}");
                            Console.Write("New departure time (leave blank to keep): ");
                            var depStr = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(depStr) && DateTime.TryParse(depStr, out DateTime newDep))
                                toUpdate.DepartureTime = newDep;

                            Console.WriteLine($"Current arrival time: {toUpdate.ArrivalTime:yyyy-MM-dd HH:mm}");
                            Console.Write("New arrival time (leave blank to keep): ");
                            var arrStr = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(arrStr) && DateTime.TryParse(arrStr, out DateTime newArr))
                                toUpdate.ArrivalTime = newArr;

                            _controller.Update(toUpdate);
                            Console.WriteLine("Flight updated successfully.");
                            break;

                        case 4:
                            Console.Write("Enter Flight ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int detId)) { Console.WriteLine("Invalid ID."); break; }
                            var flight = _controller.GetById(detId);
                            if (flight == null) { Console.WriteLine("Flight not found."); break; }

                            var depAirport = _airportController.GetById(flight.DepartureAirportId);
                            var arrAirport = _airportController.GetById(flight.ArrivalAirportId);

                            Console.WriteLine("\n=== FLIGHT DETAILS ===");
                            Console.WriteLine($"ID:             {flight.FlightId}");
                            Console.WriteLine($"Flight Number:  {flight.FlightNumber}");
                            Console.WriteLine($"Departure:      {flight.DepartureTime:yyyy-MM-dd HH:mm}");
                            Console.WriteLine($"Arrival:        {flight.ArrivalTime:yyyy-MM-dd HH:mm}");
                            Console.WriteLine($"Duration:       {(flight.ArrivalTime - flight.DepartureTime).TotalHours:F1}h");
                            Console.WriteLine($"Price:          {flight.Price:F2} EUR");
                            Console.WriteLine($"\nDeparture Airport: [{depAirport?.AirportId}] {depAirport?.Name} ({depAirport?.IATACode}) - {depAirport?.City}, {depAirport?.Country}");
                            Console.WriteLine($"Arrival Airport:   [{arrAirport?.AirportId}] {arrAirport?.Name} ({arrAirport?.IATACode}) - {arrAirport?.City}, {arrAirport?.Country}");

                            if (flight.Tickets?.Any() == true)
                            {
                                Console.WriteLine($"\nTickets sold ({flight.Tickets.Count}):");
                                foreach (var t in flight.Tickets)
                                    Console.WriteLine($"  - [{t.TicketId}] Seat: {t.SeatNumber}  Class: {t.TicketClass}  Purchased: {t.PurchaseDate:yyyy-MM-dd}");
                            }
                            else Console.WriteLine("\nNo tickets sold for this flight.");
                            break;

                        case 5:
                            Console.Write("Enter Flight ID to delete: ");
                            if (!int.TryParse(Console.ReadLine(), out int delId)) { Console.WriteLine("Invalid ID."); break; }
                            _controller.Delete(delId);
                            Console.WriteLine("Flight deleted (if it existed).");
                            break;

                        case 6: break;
                        default: Console.WriteLine("Invalid choice."); break;
                    }
                } while (op != 6);
            }
        
    }
}

