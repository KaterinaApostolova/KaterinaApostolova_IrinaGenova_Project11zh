using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirplaneTicketsApp.Controllers;
using AirplaneTicketsApp.Data.Models;

namespace AirplaneTicketsApp.Views
{
    public class TicketsView
    {
            private TicketController _controller;
            private FlightController _flightController;
            private PassengerController _passengerController;

            public TicketsView(TicketController controller, FlightController flightController, PassengerController passengerController)
            {
                _controller = controller;
                _flightController = flightController;
                _passengerController = passengerController;
            }

            public void Start()
            {
                int op;
                do
                {
                    Console.WriteLine("\n--- TICKETS ---");
                    Console.WriteLine("1. List all");
                    Console.WriteLine("2. Add ticket (link Passenger ↔ Flight)");
                    Console.WriteLine("3. Update ticket");
                    Console.WriteLine("4. Get by ID (full details)");
                    Console.WriteLine("5. Delete ticket");
                    Console.WriteLine("6. Back");
                    Console.Write("Choice: ");

                    if (!int.TryParse(Console.ReadLine(), out op)) { Console.WriteLine("Invalid input."); continue; }

                    switch (op)
                    {
                        case 1:
                            var all = _controller.GetAll();
                            if (!all.Any()) { Console.WriteLine("No tickets found."); break; }
                            Console.WriteLine($"\n{"ID",-5} {"Seat",-8} {"Class",-15} {"Purchased",-14} {"FlightID",-10} {"PassengerID"}");
                            Console.WriteLine(new string('-', 70));
                            foreach (var t in all)
                                Console.WriteLine($"{t.TicketId,-5} {t.SeatNumber,-8} {t.TicketClass,-15} {t.PurchaseDate:yyyy-MM-dd,-14} {t.FlightId,-10} {t.PassengerId}");
                            break;

                        case 2:

                            var passengers = _passengerController.GetAll();
                            if (!passengers.Any()) { Console.WriteLine("No passengers available. Add a passenger first."); break; }
                            Console.WriteLine("\nAvailable passengers:");
                            foreach (var p in passengers)
                                Console.WriteLine($"  [{p.PassengerId}] {p.FirstName} {p.LastName} ({p.PassportNumber})");

                            Console.Write("Passenger ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int passId)) { Console.WriteLine("Invalid ID."); break; }
                            if (_passengerController.GetById(passId) == null) { Console.WriteLine("Passenger not found."); break; }


                            var flights = _flightController.GetAll();
                            if (!flights.Any()) { Console.WriteLine("No flights available. Add a flight first."); break; }
                            Console.WriteLine("\nAvailable flights:");
                            foreach (var f in flights)
                                Console.WriteLine($"  [{f.FlightId}] {f.FlightNumber}  {f.DepartureTime:yyyy-MM-dd HH:mm} → {f.ArrivalTime:yyyy-MM-dd HH:mm}  Price: {f.Price:F2} EUR");

                            Console.Write("Flight ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int flightId)) { Console.WriteLine("Invalid ID."); break; }
                            if (_flightController.GetById(flightId) == null) { Console.WriteLine("Flight not found."); break; }

                            var newTicket = new Ticket
                            {
                                PassengerId = passId,
                                FlightId = flightId,
                                PurchaseDate = DateTime.Now
                            };

                            Console.Write("Seat Number (e.g. 14A): ");
                            newTicket.SeatNumber = Console.ReadLine();

                            Console.Write("Ticket Class (Economy / Business / First): ");
                            newTicket.TicketClass = Console.ReadLine();

                            _controller.Create(newTicket);
                            Console.WriteLine($"Ticket created successfully! Passenger [{passId}] linked to Flight [{flightId}].");
                            break;

                        case 3:
                            Console.Write("Enter Ticket ID to update: ");
                            if (!int.TryParse(Console.ReadLine(), out int updId)) { Console.WriteLine("Invalid ID."); break; }
                            var toUpdate = _controller.GetById(updId);
                            if (toUpdate == null) { Console.WriteLine("Ticket not found."); break; }

                            Console.WriteLine($"Current seat: {toUpdate.SeatNumber}");
                            Console.Write("New seat (leave blank to keep): ");
                            var seat = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(seat)) toUpdate.SeatNumber = seat;

                            Console.WriteLine($"Current class: {toUpdate.TicketClass}");
                            Console.Write("New class (leave blank to keep): ");
                            var cls = Console.ReadLine();
                            if (!string.IsNullOrWhiteSpace(cls)) toUpdate.TicketClass = cls;

                            _controller.Update(toUpdate);
                            Console.WriteLine("Ticket updated successfully.");
                            break;

                        case 4:
                            Console.Write("Enter Ticket ID: ");
                            if (!int.TryParse(Console.ReadLine(), out int detId)) { Console.WriteLine("Invalid ID."); break; }
                            var ticket = _controller.GetById(detId);
                            if (ticket == null) { Console.WriteLine("Ticket not found."); break; }

                            var passenger = _passengerController.GetById(ticket.PassengerId);
                            var flight = _flightController.GetById(ticket.FlightId);

                            Console.WriteLine("\n=== TICKET DETAILS ===");
                            Console.WriteLine($"Ticket ID:      {ticket.TicketId}");
                            Console.WriteLine($"Seat Number:    {ticket.SeatNumber}");
                            Console.WriteLine($"Class:          {ticket.TicketClass}");
                            Console.WriteLine($"Purchase Date:  {ticket.PurchaseDate:yyyy-MM-dd HH:mm}");

                            Console.WriteLine("\n-- Passenger --");
                            if (passenger != null)
                            {
                                Console.WriteLine($"  ID:          {passenger.PassengerId}");
                                Console.WriteLine($"  Name:        {passenger.FirstName} {passenger.LastName}");
                                Console.WriteLine($"  Passport:    {passenger.PassportNumber}");
                                Console.WriteLine($"  Email:       {passenger.Email}");
                                Console.WriteLine($"  Nationality: {passenger.Nationality}");
                            }
                            else Console.WriteLine("  (Passenger data not found)");

                            Console.WriteLine("\n-- Flight --");
                            if (flight != null)
                            {
                                Console.WriteLine($"  ID:          {flight.FlightId}");
                                Console.WriteLine($"  Number:      {flight.FlightNumber}");
                                Console.WriteLine($"  Departure:   {flight.DepartureTime:yyyy-MM-dd HH:mm}");
                                Console.WriteLine($"  Arrival:     {flight.ArrivalTime:yyyy-MM-dd HH:mm}");
                                Console.WriteLine($"  Price:       {flight.Price:F2} EUR");
                            }
                            else Console.WriteLine("  (Flight data not found)");
                            break;

                        case 5:
                            Console.Write("Enter Ticket ID to delete: ");
                            if (!int.TryParse(Console.ReadLine(), out int delId)) { Console.WriteLine("Invalid ID."); break; }
                            _controller.Delete(delId);
                            Console.WriteLine("Ticket deleted (if it existed).");
                            break;

                        case 6: break;
                        default: Console.WriteLine("Invalid choice."); break;
                    }
                } while (op != 6);
            }
        }
    
}

