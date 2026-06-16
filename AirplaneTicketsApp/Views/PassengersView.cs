using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirplaneTicketsApp.Controllers;
using AirplaneTicketsApp.Data.Models;

namespace AirplaneTicketsApp.Views
{
    public class PassengersView
    {
            private PassengerController _controller;

            public PassengersView(PassengerController controller)
            {
                _controller = controller;
            }

            public void Start()
            {
                int op;
                do
                {
                    Console.WriteLine("\n--- PASSENGERS ---");
                    Console.WriteLine("1. List all");
                    Console.WriteLine("2. Add passenger");
                    Console.WriteLine("3. Update passenger");
                    Console.WriteLine("4. Get by ID (full details)");
                    Console.WriteLine("5. Delete passenger");
                    Console.WriteLine("6. Back");
                    Console.Write("Choice: ");

                    if (!int.TryParse(Console.ReadLine(), out op)) { Console.WriteLine("Invalid input."); continue; }

                    switch (op)
                    {
                        case 1:
                            var all = _controller.GetAll();
                            if (!all.Any()) { Console.WriteLine("No passengers found."); break; }
                            Console.WriteLine($"\n{"ID",-5} {"First Name",-20} {"Last Name",-20} {"Passport",-15} {"Nationality",-20} {"Email"}");
                            Console.WriteLine(new string('-', 100));
                            foreach (var p in all)
                                Console.WriteLine($"{p.PassengerId,-5} {p.FirstName,-20} {p.LastName,-20} {p.PassportNumber,-15} {p.Nationality,-20} {p.Email}");
                            break;

                        case 2:
                            var newP = new Passenger();
                            Console.Write("First Name: ");
                            newP.FirstName = Console.ReadLine();
                            Console.Write("Last Name: ");
                            newP.LastName = Console.ReadLine();
                            Console.Write("Passport Number: ");
                            newP.PassportNumber = Console.ReadLine();
                            Console.Write("Email: ");
                            newP.Email = Console.ReadLine();
                            Console.Write("Nationality: ");
                            newP.Nationality = Console.ReadLine();
                            _controller.Create(newP);
                            Console.WriteLine("Passenger added successfully.");
                            break;

                        case 3:
                            Console.Write("Enter Passenger ID to update: ");
                            if (!int.TryParse(Console.ReadLine(), out int updId)) { Console.WriteLine("Invalid ID."); break; }
                            var toUpdate = _controller.GetById(updId);
                            if (toUpdate == null) { Console.WriteLine("Passenger not found."); break; }

                            Console.WriteLine($"Current first name: {toUpdate.FirstName}");
                            Console.Write("New first name (leave blank to keep): ");
                            var fn = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(fn)) toUpdate.FirstName = fn;

                            Console.WriteLine($"Current last name: {toUpdate.LastName}");
                            Console.Write("New last name (leave blank to keep): ");
                            var ln = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(ln)) toUpdate.LastName = ln;

                            Console.WriteLine($"Current email: {toUpdate.Email}");
                            Console.Write("New email (leave blank to keep): ");
                            var em = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(em)) toUpdate.Email = em;

                            Console.WriteLine($"Current passport: {toUpdate.PassportNumber}");
                            Console.Write("New passport number (leave blank to keep): ");
                            var pp = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(pp)) toUpdate.PassportNumber = pp;

                            Console.WriteLine($"Current nationality: {toUpdate.Nationality}");
                            Console.Write("New nationality (leave blank to keep): ");
                            var nat = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(nat)) toUpdate.Nationality = nat;

                            _controller.Update(toUpdate);
                            Console.WriteLine("Passenger updated successfully.");
                            break;

                        case 4:
                            Console.Write("Enter Passenger ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int detId)) { Console.WriteLine("Invalid ID."); break; }
                            var passenger = _controller.GetById(detId);
                            if (passenger == null) { Console.WriteLine("Passenger not found."); break; }

                            Console.WriteLine("\n=== PASSENGER DETAILS ===");
                            Console.WriteLine($"ID:              {passenger.PassengerId}");
                            Console.WriteLine($"Full Name:       {passenger.FirstName} {passenger.LastName}");
                            Console.WriteLine($"Passport Number: {passenger.PassportNumber}");
                            Console.WriteLine($"Email:           {passenger.Email}");
                            Console.WriteLine($"Nationality:     {passenger.Nationality}");

                            if (passenger.Tickets?.Any() == true)
                            {
                                Console.WriteLine($"\nTickets ({passenger.Tickets.Count}):");
                                foreach (var t in passenger.Tickets)
                                {
                                    Console.WriteLine($"  - [{t.TicketId}] Flight: {t.Flight?.FlightNumber ?? "N/A"}  " +
                                        $"Seat: {t.SeatNumber}  Class: {t.TicketClass}  Purchased: {t.PurchaseDate:yyyy-MM-dd}");
                                }
                            }
                            else Console.WriteLine("\nNo tickets for this passenger.");
                            break;

                        case 5:
                            Console.Write("Enter Passenger ID to delete: ");
                            if (!int.TryParse(Console.ReadLine(), out int delId)) { Console.WriteLine("Invalid ID."); break; }
                            _controller.Delete(delId);
                            Console.WriteLine("Passenger deleted (if it existed).");
                            break;

                        case 6: break;
                        default: Console.WriteLine("Invalid choice."); break;
                    }
                } while (op != 6);
            }
        
    
    }
}
