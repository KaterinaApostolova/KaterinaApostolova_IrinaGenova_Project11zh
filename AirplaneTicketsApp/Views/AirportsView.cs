using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirplaneTicketsApp.Controllers;
using AirplaneTicketsApp.Data.Models;

namespace AirplaneTicketsApp.Views
{
        public class AirportsView
        {
            private AirportController _controller;

            public AirportsView(AirportController controller)
            {
                _controller = controller;
            }

            public void Start()
            {
                int op;
                do
                {
                    Console.WriteLine("\n--- AIRPORTS ---");
                    Console.WriteLine("1. List all");
                    Console.WriteLine("2. Add airport");
                    Console.WriteLine("3. Update airport");
                    Console.WriteLine("4. Get by ID (full details)");
                    Console.WriteLine("5. Delete airport");
                    Console.WriteLine("6. Back");
                    Console.Write("Choice: ");

                    if (!int.TryParse(Console.ReadLine(), out op)) { Console.WriteLine("Invalid input."); continue; }

                    switch (op)
                    {
                        case 1:
                            var all = _controller.GetAll();
                            if (!all.Any()) { Console.WriteLine("No airports found."); break; }
                            Console.WriteLine($"\n{"ID",-5} {"Name",-30} {"City",-20} {"Country",-20} {"IATA",-6} {"Capacity",-10}");
                            Console.WriteLine(new string('-', 95));
                            foreach (var a in all)
                                Console.WriteLine($"{a.AirportId,-5} {a.Name,-30} {a.City,-20} {a.Country,-20} {a.IATACode,-6} {a.Capacity,-10}");
                            break;

                        case 2:
                            var newAirport = new Airport();
                            Console.Write("Name: ");
                            newAirport.Name = Console.ReadLine();
                            Console.Write("City: ");
                            newAirport.City = Console.ReadLine();
                            Console.Write("Country: ");
                            newAirport.Country = Console.ReadLine();
                            Console.Write("IATA Code (e.g. SOF): ");
                            newAirport.IATACode = Console.ReadLine();
                            Console.Write("Capacity: ");
                            if (!int.TryParse(Console.ReadLine(), out int cap)) { Console.WriteLine("Invalid capacity."); break; }
                            newAirport.Capacity = cap;
                            _controller.Create(newAirport);
                            Console.WriteLine("Airport added successfully.");
                            break;

                        case 3:
                            Console.Write("Enter Airport ID to update: ");
                            if (!int.TryParse(Console.ReadLine(), out int updId)) { Console.WriteLine("Invalid ID."); break; }
                            var toUpdate = _controller.GetById(updId);
                            if (toUpdate == null) { Console.WriteLine("Airport not found."); break; }

                            Console.WriteLine($"Current name: {toUpdate.Name}");
                            Console.Write("New name (leave blank to keep): ");
                            var newName = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newName)) toUpdate.Name = newName;

                            Console.WriteLine($"Current city: {toUpdate.City}");
                            Console.Write("New city (leave blank to keep): ");
                            var newCity = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newCity)) toUpdate.City = newCity;

                            Console.WriteLine($"Current country: {toUpdate.Country}");
                            Console.Write("New country (leave blank to keep): ");
                            var newCountry = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newCountry)) toUpdate.Country = newCountry;

                            Console.WriteLine($"Current IATA: {toUpdate.IATACode}");
                            Console.Write("New IATA (leave blank to keep): ");
                            var newIata = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(newIata)) toUpdate.IATACode = newIata;

                            Console.WriteLine($"Current capacity: {toUpdate.Capacity}");
                            Console.Write("New capacity (leave blank to keep): ");
                            var capStr = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(capStr) && int.TryParse(capStr, out int newCap))
                                toUpdate.Capacity = newCap;

                            _controller.Update(toUpdate);
                            Console.WriteLine("Airport updated successfully.");
                            break;

                        case 4:
                            Console.Write("Enter Airport ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int detId)) { Console.WriteLine("Invalid ID."); break; }
                            var airport = _controller.GetById(detId);
                            if (airport == null) { Console.WriteLine("Airport not found."); break; }

                            Console.WriteLine("\n=== AIRPORT DETAILS ===");
                            Console.WriteLine($"ID:       {airport.AirportId}");
                            Console.WriteLine($"Name:     {airport.Name}");
                            Console.WriteLine($"City:     {airport.City}");
                            Console.WriteLine($"Country:  {airport.Country}");
                            Console.WriteLine($"IATA:     {airport.IATACode}");
                            Console.WriteLine($"Capacity: {airport.Capacity}");

                            if (airport.DepartureFlights?.Any() == true)
                            {
                                Console.WriteLine($"\nDeparture Flights ({airport.DepartureFlights.Count}):");
                                foreach (var f in airport.DepartureFlights)
                                    Console.WriteLine($"  - [{f.FlightId}] {f.FlightNumber}  Departs: {f.DepartureTime:yyyy-MM-dd HH:mm}  Price: {f.Price:F2}");
                            }
                            else Console.WriteLine("\nNo departure flights.");

                            if (airport.ArrivalFlights?.Any() == true)
                            {
                                Console.WriteLine($"\nArrival Flights ({airport.ArrivalFlights.Count}):");
                                foreach (var f in airport.ArrivalFlights)
                                    Console.WriteLine($"  - [{f.FlightId}] {f.FlightNumber}  Arrives: {f.ArrivalTime:yyyy-MM-dd HH:mm}  Price: {f.Price:F2}");
                            }
                            else Console.WriteLine("No arrival flights.");
                            break;

                        case 5:
                            Console.Write("Enter Airport ID to delete: ");
                            if (!int.TryParse(Console.ReadLine(), out int delId)) { Console.WriteLine("Invalid ID."); break; }
                            _controller.Delete(delId);
                            Console.WriteLine("Airport deleted (if it existed).");
                            break;

                        case 6: break;
                        default: Console.WriteLine("Invalid choice."); break;
                    }
                } while (op != 6);
            }
        }

}

